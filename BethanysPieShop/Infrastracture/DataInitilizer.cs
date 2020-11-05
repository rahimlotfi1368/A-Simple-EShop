using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace Infrastracture
{
    public static class DataInitilizer:System.Object
    {
       public static  void SeedData(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager) 
       {
            SeedRole(roleManager);
            SeedUser(userManager);
       }

       public static  void SeedRole(RoleManager<IdentityRole> roleManager)
        {
            
            if (!roleManager.RoleExistsAsync(Resources.DataDictionary.RoleName1).Result)
            {
                IdentityResult result;
                IdentityRole identityRole = new IdentityRole()
                {
                    Name=Resources.DataDictionary.RoleName1,
                };
                result = roleManager.CreateAsync(identityRole).Result;
                
            }

            if (!roleManager.RoleExistsAsync(Resources.DataDictionary.RoleName2).Result)
            {
                IdentityResult result;
                IdentityRole identityRole = new IdentityRole()
                {
                    Name = Resources.DataDictionary.RoleName2,
                };
                result = roleManager.CreateAsync(identityRole).Result;

            }

            if (!roleManager.RoleExistsAsync(Resources.DataDictionary.RoleName3).Result)
            {
                IdentityResult result;
                IdentityRole identityRole = new IdentityRole()
                {
                    Name = Resources.DataDictionary.RoleName3,
                };
                result = roleManager.CreateAsync(identityRole).Result;

            }

            if (!roleManager.RoleExistsAsync(Resources.DataDictionary.RoleName4).Result)
            {
                IdentityResult result;
                IdentityRole identityRole = new IdentityRole()
                {
                    Name = Resources.DataDictionary.RoleName4,
                };
                result = roleManager.CreateAsync(identityRole).Result;

            }
        }

        public static  void SeedUser(UserManager<IdentityUser> userManager)
        {

        }
    }
}
