using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccessLayer.BethanysPieShop
{
    public class PieRepository:Repository<Models.Pie>,IPieRepository
    {
        public PieRepository(Models.DataBaseContext dataBaseContext):base(dataBaseContext)
        {

        }

        public IEnumerable<Pie> GetPiesByCatagory(string catagoryName)
        {
            var piesByCatagory = DataBaseContext.Pies
                               .Where(current => current.PieCatagory.CatagoryName.ToLower() == catagoryName.ToLower())
                               ;

            return piesByCatagory;
        }

        public IEnumerable<Pie> GetPiesOftheWeekl(int take = 5)
        {
            var piesOfCarousel = DataBaseContext.Pies
                                 .OrderByDescending(current => current.Visit)
                                 .Where(current=>current.IsPieOfTheWeek==true)
                                 .Take(take);

            return piesOfCarousel;
        }

        
    }
}
