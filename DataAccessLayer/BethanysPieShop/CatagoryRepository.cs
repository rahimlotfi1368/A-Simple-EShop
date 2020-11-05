using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.BethanysPieShop
{
    public class CatagoryRepository:Repository<Models.Catagory>,ICatagoryRepository
    {
        public CatagoryRepository(Models.DataBaseContext dataBaseContext):base(dataBaseContext)
        {

        }
    }
}
