using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace BethanysPieShop.Components
{
    public class AdministrationPanelAccess:Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public AdministrationPanelAccess():base()
        {
            
        }

        public Microsoft.AspNetCore.Mvc.IViewComponentResult Invoke()
        {
              return View();
        }
        
        
    }
}
