using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace BethanysPieShop.Areas.Administration.Controllers
{
    [Microsoft.AspNetCore.Mvc.Area("Administration")]
    [Microsoft.AspNetCore.Authorization.Authorize()]
    public class ShopingController : Infrastracture.BaseController
    {
        private readonly SignInManager<IdentityUser> signInManager;

        public ShopingController(SignInManager<IdentityUser> signInManager)
            :base()
        {
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult AddToCart(System.Guid Id)
        {
            var theUser = UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.GetUserByUsername(User.Identity.Name);
            var theOrder = UnitOfWork.BethanyPieShopUnitOfWork.OrderRepository.GetOrderByUserId(theUser.Id);
            var selectedPie = UnitOfWork.BethanyPieShopUnitOfWork.PieRepository.GetById(Id);
            if (theOrder==null)
            {
                Models.Order newOrder = new Models.Order()
                {
                    UserId=theUser.Id,
                    IsFinaly=false,
                    Sum=0,
                };

                Models.OrderDetails newOrderDetails = new Models.OrderDetails()
                {
                    OrderId=newOrder.Id,
                    PieId=selectedPie.Id,
                    Price=selectedPie.Price,
                    Amount=1,
                    
                };
                
                UnitOfWork.BethanyPieShopUnitOfWork.OrderRepository.Insert(newOrder);
                UnitOfWork.BethanyPieShopUnitOfWork.OrderDetailsRepository.Insert(newOrderDetails);
                UnitOfWork.BethanyPieShopUnitOfWork.OrderDetailsRepository.Save();
                UnitOfWork.BethanyPieShopUnitOfWork.OrderRepository.Save();
            }
            else
            {
                var theOrderDetails = UnitOfWork.BethanyPieShopUnitOfWork.OrderDetailsRepository.GetOrderDetailsByPieIdAndOrderId(selectedPie.Id, theOrder.Id );
                if (theOrderDetails==null)
                {
                    Models.OrderDetails orderDetails = new Models.OrderDetails()
                    {
                        OrderId=theOrder.Id,
                        PieId=selectedPie.Id,
                        Price=selectedPie.Price,
                        Amount=1,
                    };
                    
                    UnitOfWork.BethanyPieShopUnitOfWork.OrderDetailsRepository.Insert(orderDetails);
                }
                else
                {
                    ++theOrderDetails.Amount;
                     UnitOfWork.BethanyPieShopUnitOfWork.OrderDetailsRepository.Update(theOrderDetails);
                }
                UnitOfWork.BethanyPieShopUnitOfWork.OrderDetailsRepository.Save();
            }
            UnitOfWork.BethanyPieShopUnitOfWork.OrderRepository.UpdateSumOfOrder(theOrder);
            return RedirectToAction(actionName: nameof(Index), controllerName: "Home", routeValues: new { area = "" });

        }

        [HttpGet]
        public ActionResult ShowCart()
        {
            List<ViewModels.ShowCartViewModel> orderDetailsList = new List<ViewModels.ShowCartViewModel>();
            var theUser = UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.GetUserByUsername(User.Identity.Name);
            var theOrder = UnitOfWork.BethanyPieShopUnitOfWork.OrderRepository.GetOrderByUserId(theUser.Id);
            
            if (theOrder!=null)
            {
                var details = UnitOfWork.BethanyPieShopUnitOfWork.OrderDetailsRepository.GetOrderDetailsByOrderId(theOrder.Id);
                foreach (var item in details)
                {
                    var viewModel = new ViewModels.ShowCartViewModel()
                    {
                        OrderDetails = item,
                        SumOfEachOrderDetails =item.Price*item.Amount,
                       ImageName=UnitOfWork.BethanyPieShopUnitOfWork.PieRepository.GetById(item.PieId).ImageName
                    };
                    orderDetailsList.Add(viewModel);
                }
            }
            else
            {
                return RedirectToAction(actionName: nameof(Index), controllerName: "Home", routeValues: new { area = "" });
            }

            decimal sum = 0;
            foreach (var item in orderDetailsList)
            {
                sum += item.SumOfEachOrderDetails;
            }

            ViewBag.TotalOrderPrice = sum;
            ViewBag.TheOrderId = theOrder.Id;
            return View(orderDetailsList);
        }
        [HttpGet]
        public ActionResult Command(System.Guid Id,string command)
        {
            var theOrderDetail = UnitOfWork.BethanyPieShopUnitOfWork.OrderDetailsRepository.GetById(Id);
            switch (command)
            {
                case "Increase":
                    {
                        ++theOrderDetail.Amount;
                        UnitOfWork.BethanyPieShopUnitOfWork.OrderDetailsRepository.Update(theOrderDetail);
                        break;
                    }

                case "decrease":
                    {
                        --theOrderDetail.Amount;
                        if (theOrderDetail.Amount <= 0)
                        {
                            UnitOfWork.BethanyPieShopUnitOfWork.OrderDetailsRepository.Delete(theOrderDetail);
                        }
                        else
                        {
                            UnitOfWork.BethanyPieShopUnitOfWork.OrderDetailsRepository.Update(theOrderDetail);
                        }
                        break;
                    }
                default:
                    break;
            }
            UnitOfWork.BethanyPieShopUnitOfWork.OrderDetailsRepository.Save();
            return RedirectToAction(actionName: nameof(ShowCart), "Shoping");
        }

        [HttpGet]
        public ActionResult CheckOut(System.Guid orderId,decimal orderSum)
        {
            var theOrder = UnitOfWork.BethanyPieShopUnitOfWork.OrderRepository.GetById(orderId);
            var theUser = UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.GetUserByUsername(User.Identity.Name);
            if (theOrder==null || orderSum<=0)
            {
                return NotFound();
            }

            theOrder.IsFinaly = true;
            theOrder.Sum = orderSum;
            theUser.TotalShoping += orderSum;
            UnitOfWork.BethanyPieShopUnitOfWork.OrderRepository.Update(theOrder);
            UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.Update(theUser);
            UnitOfWork.BethanyPieShopUnitOfWork.OrderRepository.Save();
            UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.Save();
            return View();
        }

        [HttpGet]
        public ActionResult DeleteFromCart(System.Guid Id)
        {
            UnitOfWork.BethanyPieShopUnitOfWork.OrderDetailsRepository.DeleteById(Id);
            UnitOfWork.BethanyPieShopUnitOfWork.OrderDetailsRepository.Save();
            return RedirectToAction(actionName: nameof(ShowCart), "Shoping");
        }

    }
}
