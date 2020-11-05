using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Components
{
    public class ShowProfilImage:Microsoft.AspNetCore.Mvc.ViewComponent
    {
        public ShowProfilImage():base()
        {

        }

        public Microsoft.AspNetCore.Mvc.IViewComponentResult Invoke()
        {
            var theUser = UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.GetUserByUsername(User.Identity.Name);
            return View(theUser);
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
