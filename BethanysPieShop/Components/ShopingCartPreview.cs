using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Components
{
    public class ShopingCartPreview:Microsoft.AspNetCore.Mvc.ViewComponent
    {
        public ShopingCartPreview()
            :base()
        {

        }

        public Microsoft.AspNetCore.Mvc.IViewComponentResult Invoke()
        {
            List<ViewModels.ShowCartViewModel> viewModel = new List<ViewModels.ShowCartViewModel>();
            var theUser = UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.GetUserByUsername(User.Identity.Name);
            var theOrder = UnitOfWork.BethanyPieShopUnitOfWork.OrderRepository.GetOrderByUserId(theUser.Id);
            if (theOrder != null)
            {
                var details = unitOfWork.BethanyPieShopUnitOfWork.OrderDetailsRepository.GetOrderDetailsByOrderId(theOrder.Id);
                foreach (var item in details)
                {
                    ViewModels.ShowCartViewModel showCartViewModel = new ViewModels.ShowCartViewModel()
                    {
                        OrderDetails=item,
                        SumOfEachOrderDetails=item.Price*item.Amount,
                        ImageName=unitOfWork.BethanyPieShopUnitOfWork.PieRepository.GetById(item.PieId).ImageName,
                    };

                    viewModel.Add(showCartViewModel);
                }
            }
            decimal sum = 0;
            foreach (var item in viewModel)
            {
                sum += item.SumOfEachOrderDetails;
            }

            ViewBag.TotalOrderPrice = sum;
            ViewBag.UserImage = theUser.ImageName;
            return View(viewModel);
        }
        //************************************************************************
        private Models.DataBaseContext dataBaseContext;
        public Models.DataBaseContext DataBaseContext
        {
            get
            {
                if (dataBaseContext == null)
                {
                    dataBaseContext = new Models.DataBaseContext();
                }

                return dataBaseContext;
            }
        }

        private DataAccessLayer.UnitOfWork unitOfWork;

        public DataAccessLayer.UnitOfWork UnitOfWork
        {
            get
            {
                if (unitOfWork == null)
                {
                    unitOfWork = new DataAccessLayer.UnitOfWork(dataBaseContext);
                }

                return unitOfWork;
            }
        }
    }
}
