using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BethanysPieShop.Areas.Administration.Controllers
{
    [Microsoft.AspNetCore.Mvc.Area("Administration")]
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin,Boss")]
    //[Microsoft.AspNetCore.Authorization.Authorize()]
    public class ManageUserController : Infrastracture.BaseController
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public ManageUserController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager) : base()
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public async Task<Microsoft.AspNetCore.Mvc.IActionResult> ListAllUsers()
        {
            var usersList = UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.Get(includeProperties: nameof(Models.User.Role));
            foreach (var item in usersList)
            {
                item.Role = await roleManager.FindByIdAsync(item.RoleId.ToString());
            }
            return View(model: usersList);
        }

        [HttpGet]
        public async Task<Microsoft.AspNetCore.Mvc.IActionResult> AddUserToRole(string userName)
        {
            if (userName==null)
            {
                return NotFound();
            }

            var user = await userManager.FindByNameAsync(userName);
            
            if (user == null )
            {
                return NotFound();
            }
            var roles =await roleManager.Roles.ToListAsync();

            var viewModel = new ViewModels.UserManagerViewModel()
            {
                UserId = user.Id
            };

            foreach (var item in roles)
            {
                if (!await userManager.IsInRoleAsync(user, item.Name))
                {
                    
                    viewModel.UserRoles.Add(item.Name);

                }
            }
            
            ViewData["Roles"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(viewModel.UserRoles, "RoleName");
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Microsoft.AspNetCore.Mvc.IActionResult> AddUserToRole(ViewModels.UserManagerViewModel viewModel)
        {

            if (viewModel == null)
            {
                return NotFound();
            }

            var user = await userManager.FindByIdAsync(viewModel.UserId);
            var fulluser = UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.GetUserByUsername(user.UserName);
            if (user == null)
            {
                return NotFound();
            }

            var requestedRole = viewModel.RoleName;

            bool roleExistResult = await roleManager.RoleExistsAsync(requestedRole);
            if (!roleExistResult)
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = requestedRole,
                };
                var roleResult = await roleManager.CreateAsync(role);
            }
            if (fulluser.RoleId==Guid.Empty)
            {
                var result = await userManager.AddToRoleAsync(user, requestedRole);

                if (result.Succeeded)
                {
                    fulluser.RoleId = Guid.Parse(roleManager.FindByNameAsync(requestedRole).Result.Id);
                    UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.Update(fulluser);
                    UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.Save();
                    viewModel.RoleId = fulluser.RoleId;
                    return RedirectToAction(actionName: "ListAllUsers", controllerName: "ManageUser");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            viewModel.RoleId = fulluser.RoleId;
           return View(viewModel);

        }

        [HttpGet]
        public async Task<Microsoft.AspNetCore.Mvc.IActionResult> RemoveUserFromRole(string userName)
        {
            if (userName == null)
            {
                return NotFound();
            }

            var user = await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return NotFound();
            }
            var roles = await roleManager.Roles.ToListAsync();

            var viewModel = new ViewModels.UserManagerViewModel()
            {
                UserId = user.Id
            };

            foreach (var item in roles)
            {
                if (await userManager.IsInRoleAsync(user, item.Name))
                {

                    viewModel.UserRoles.Add(item.Name);

                }
            }

            ViewData["Roles"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(viewModel.UserRoles, "RoleName");
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Microsoft.AspNetCore.Mvc.IActionResult> RemoveUserFromRole(ViewModels.UserManagerViewModel viewModel)
        {
            if (viewModel == null)
            {
                return NotFound();
            }

            var user = await userManager.FindByIdAsync(viewModel.UserId);
            var fulluser = UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.GetUserByUsername(user.UserName);
            if (user == null)
            {
                return NotFound();
            }

            var requestedRole = viewModel.RoleName;
                       
            if (fulluser.RoleId != Guid.Empty)
            {
                var result = await userManager.RemoveFromRoleAsync(user, requestedRole);

                if (result.Succeeded)
                {
                    fulluser.RoleId = Guid.Empty;
                    UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.Update(fulluser);
                    UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.Save();
                    viewModel.RoleId = fulluser.RoleId;
                    return RedirectToAction(actionName: "ListAllUsers", controllerName: "ManageUser");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            viewModel.RoleId = fulluser.RoleId;
            return View(viewModel);
        }

            [HttpGet]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.GetById(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var user = UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.GetById(id);
            if (user.ImageName != null)
            {
                var filePath = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot", "Images"))
                                                                     .Root + $@"\{user.ImageName}";
                System.IO.File.Delete(filePath);
            }
            if (User.Identity.Name.ToLower() == user.Username.ToLower())
            {
                var currentUser = await userManager.FindByNameAsync(User.Identity.Name);
                await userManager.DeleteAsync(currentUser);
                await signInManager.SignOutAsync();
            }
            else
            {
                var aspUser= await userManager.FindByNameAsync(user.Username);
                await userManager.DeleteAsync(aspUser);
            }
            UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.Delete(user);
            UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.Save();
            return RedirectToAction(nameof(ListAllUsers));
        }

        [HttpGet]
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.GetUserById(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}
