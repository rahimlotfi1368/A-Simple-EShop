//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
namespace Infrastracture
{
    public class BaseController : Microsoft.AspNetCore.Mvc.Controller
    {
        public BaseController():base()
        {

        }

        private Models.DataBaseContext dataBaseContext;
        public Models.DataBaseContext DataBaseContext 
        {
            get
            {
                if (dataBaseContext==null)
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

        protected override void Dispose(bool disposing)
        {
            if (dataBaseContext != null)
            {
                dataBaseContext.Dispose();
                dataBaseContext = null;
            }
            base.Dispose(disposing);
        }
    }
}
