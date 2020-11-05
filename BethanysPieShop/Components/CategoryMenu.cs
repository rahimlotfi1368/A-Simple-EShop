using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Components
{
    public class CategoryMenu:Microsoft.AspNetCore.Mvc.ViewComponent
    {
        public CategoryMenu():base()
        {

        }

        public Microsoft.AspNetCore.Mvc.IViewComponentResult Invoke()
        {
            var categoryList = UnitOfWork.BethanyPieShopUnitOfWork.CatagoryRepository.Get();
            return View(categoryList);
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
