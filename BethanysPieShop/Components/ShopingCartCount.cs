using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace Components
{
    public class ShopingCartCount:Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly SignInManager<IdentityUser> signInManager;

        public ShopingCartCount(SignInManager<IdentityUser> signInManager)
            :base()
        {
            this.signInManager = signInManager;
        }

        public Microsoft.AspNetCore.Mvc.IViewComponentResult Invoke()
        {
            List<Models.OrderDetails> details=new List<Models.OrderDetails>();
            var theUser = UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.GetUserByUsername(User.Identity.Name);
            var theOrder = UnitOfWork.BethanyPieShopUnitOfWork.OrderRepository.GetOrderByUserId(theUser.Id);
            if (theOrder!=null)
            {
                 details = unitOfWork.BethanyPieShopUnitOfWork.OrderDetailsRepository.GetOrderDetailsByOrderId(theOrder.Id).ToList();
                ViewBag.CartItemsCount = details.Count();
            }
            return View();
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
