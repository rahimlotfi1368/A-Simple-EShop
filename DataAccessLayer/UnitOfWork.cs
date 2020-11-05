using System.Linq;
namespace DataAccessLayer
{
    public class UnitOfWork: object, IUnitOfWork
    {
        //**************************************************************
        protected Models.DataBaseContext dataBaseContext { get; set; }
        public UnitOfWork(Models.DataBaseContext dataBaseContext)
        {
            if (dataBaseContext==null)
            {
                dataBaseContext = new Models.DataBaseContext();
            }

            this.dataBaseContext = dataBaseContext;
        }
        //**************************************************************

        private BethanysPieShop.IUnitOfWork bethanyPieShopUnitOfWork;

        public BethanysPieShop.IUnitOfWork BethanyPieShopUnitOfWork
        {
            get
            {
                if (bethanyPieShopUnitOfWork == null)
                {
                    bethanyPieShopUnitOfWork =
                                new BethanysPieShop.UnitOfWork(dataBaseContext);
                }

                return (bethanyPieShopUnitOfWork);
            }

        }


        //************************************************************************************ 
        //the below code should write for each MicroProject Unit Of work here RoleUserManager
        //************************************************************************************
        //private RoleUserManager.IUnitOfWork roleUserManagerUnitOfWork;

        //public RoleUserManager.IUnitOfWork RoleUserManagerUnitOfWork
        //{
        //    get
        //    {
        //        if (roleUserManagerUnitOfWork == null)
        //        {
        //            roleUserManagerUnitOfWork =
        //                        new RoleUserManager.UnitOfWork(DatabaseContext);
        //        }

        //        return (roleUserManagerUnitOfWork);
        //    }

        //}

        //************************************************************** 
    }
}
