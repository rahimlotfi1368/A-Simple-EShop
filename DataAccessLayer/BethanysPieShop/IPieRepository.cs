using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.BethanysPieShop
{
    public interface IPieRepository:IRepository<Models.Pie>
    {
        System.Collections.Generic.IEnumerable<Models.Pie> GetPiesOftheWeekl(int take = 5);
        System.Collections.Generic.IEnumerable<Models.Pie> GetPiesByCatagory(string catagoryName);

    }
}
