//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
    public class HomeController : Infrastracture.BaseController
    {
        public HomeController():base()
        {
            
        }
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public Microsoft.AspNetCore.Mvc.IActionResult Index()
        {
            var carouselPies = UnitOfWork.BethanyPieShopUnitOfWork.PieRepository.GetPiesOftheWeekl();
            return View(model:carouselPies);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public Microsoft.AspNetCore.Mvc.IActionResult ShowDetailsOfPie(System.Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var thePie = UnitOfWork.BethanyPieShopUnitOfWork.PieRepository.GetById(id.Value);
            ++thePie.Visit;
            UnitOfWork.BethanyPieShopUnitOfWork.PieRepository.Update(thePie);
            UnitOfWork.BethanyPieShopUnitOfWork.PieRepository.Save();
            return View(model: thePie);
        }
    }
}
