using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace BethanysPieShop.Controllers
{
    public class AccountController : Infrastracture.BaseController
    {


        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager) : base()
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public Microsoft.AspNetCore.Mvc.IActionResult LogIn()
        {
            var isSignIn = signInManager.IsSignedIn(User);
            if (isSignIn)
            {
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            return View();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(ViewModels.LoginViewModel viewModel)
        {
            var isSignIn = signInManager.IsSignedIn(User);
            if (isSignIn)
            {
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            ViewBag.returnUrl = viewModel.ReturnUrl;

            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync
                                       (viewModel.Username, viewModel.Password, isPersistent: viewModel.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrWhiteSpace(viewModel.ReturnUrl) && Url.IsLocalUrl(viewModel.ReturnUrl))
                    {
                        return Redirect(viewModel.ReturnUrl);
                    }

                    return RedirectToAction(actionName: "Index", controllerName: "Home");
                }
                if (result.IsLockedOut)
                {
                    ViewBag.ErrorMessage = Resources.Messages.LockOutError;
                    return View(viewModel);
                }

                ModelState.AddModelError("", Resources.Messages.WrongPasswordError);
            }
            return View(model:viewModel);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public Microsoft.AspNetCore.Mvc.IActionResult Registrar()
        {
            //var roleSelectList =await roleManager.Roles.ToListAsync();
            //ViewData["RoleId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(roleSelectList, "Id", "Name");

            return View();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public async Task<Microsoft.AspNetCore.Mvc.IActionResult> Registrar(ViewModels.RegisterViewModel viewModel)
        {
            var roleSelectList = await roleManager.Roles.ToListAsync();
            System.Collections.Generic.List<string> roleNames = new System.Collections.Generic.List<string>();
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName=viewModel.Username,
                    NormalizedUserName=viewModel.Username.ToUpper(),
                    Email=viewModel.EMail,
                    EmailConfirmed=true
                };

                var newUser = new Models.User()
                {
                    Username = viewModel.Username,
                    Password = viewModel.Password,
                    EMail = viewModel.EMail,
                    PhoneNumber = viewModel.PhoneNumber,
                    FullName = viewModel.FullName,
                    Address = viewModel.Address,
                    City = viewModel.City,
                    Country = viewModel.Country,
                    ZipCode = viewModel.ZipCode,
                    State = viewModel.State,
                    IsActive = viewModel.IsActive,
                    //RoleId =Guid.Parse( roleSelectList.Where(current => current.Name == Resources.DataDictionary.RoleName3).FirstOrDefault().Id),
                    TotalShoping=0,
                    
                };
                
                if (viewModel.ImageUpload != null)
                {
                    newUser.ImageName = $"{newUser.Id}{System.IO.Path.GetExtension(viewModel.ImageUpload.FileName)}";
                    var filePath = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot", "Images"))
                                                                       .Root + $@"\{newUser.ImageName}";

                    using (System.IO.FileStream fileStream = System.IO.File.Create(filePath))
                    {
                        viewModel.ImageUpload.CopyTo(fileStream);
                        fileStream.Flush();
                    }

                }
                UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.Insert(newUser);
                var isSavedSuccessfully = UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.Save();
                var result = await userManager.CreateAsync(user, viewModel.Password);
                
                if (isSavedSuccessfully && result.Succeeded)
                {
                   
                    return RedirectToAction(actionName: "Index", controllerName: "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            //var roleSelectList = await roleManager.Roles.ToListAsync();
            //ViewData["RoleId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(roleSelectList, "Id", "Name",viewModel.RoleId);

            return View(viewModel);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        [HttpGet]
        public async Task<Microsoft.AspNetCore.Mvc.IActionResult> ShowProfileInformation()
        {
            System.Collections.Generic.List<Models.OrderDetails> orderDetailsList = 
                                                                    new System.Collections.Generic.List<Models.OrderDetails>();

            ViewModels.ShowProfileInformationViewModel viewModel = new ViewModels.ShowProfileInformationViewModel();
            
            var theUser = UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.GetUserByUsername(User.Identity.Name);
            var theOrder = UnitOfWork.BethanyPieShopUnitOfWork.OrderRepository.GetAllOrdersByUserId(theUser.Id);
            var roleSelectList = await roleManager.Roles.ToListAsync();
            theUser.Role= roleSelectList.Where(current => Guid.Parse(current.Id) == theUser.RoleId).FirstOrDefault();
            viewModel.User = theUser;
            viewModel.UserOrders = theOrder;
            foreach (var item in theOrder)
            {
                if (item!=null)
                {
                    var details = UnitOfWork.BethanyPieShopUnitOfWork.OrderDetailsRepository.GetOrderDetailsByOrderId(item.Id);
                    foreach (var detail in details)
                    {
                        Models.OrderDetails orderDetails = new Models.OrderDetails()
                        {
                            Id=detail.Id,
                            Amount=detail.Amount,
                            Price=detail.Price,
                            PieId=detail.PieId,
                            OrderId=detail.OrderId,
                            Pie= UnitOfWork.BethanyPieShopUnitOfWork.PieRepository.GetById(detail.PieId)
                        };
                        orderDetailsList.Add(orderDetails);
                    }
                    viewModel.UserOrderDetails = orderDetailsList;
                }
            }


            ViewBag.FullAdress = $"{viewModel.User.Country},{viewModel.User.State},{viewModel.User.City},{viewModel.User.Address},{viewModel.User.ZipCode}";
            return View(viewModel);
        }

       
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public Microsoft.AspNetCore.Mvc.IActionResult DeleteOrder(System.Guid id)
        {
            var order = UnitOfWork.BethanyPieShopUnitOfWork.OrderRepository.GetById(id);
            var theUser = UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.GetUserByUsername(User.Identity.Name);
            if (theUser.TotalShoping>0)
            {
                theUser.TotalShoping -= order.Sum;
                UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.Update(theUser);
                UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.Save();
            }
            UnitOfWork.BethanyPieShopUnitOfWork.OrderRepository.Delete(order);
            UnitOfWork.BethanyPieShopUnitOfWork.OrderRepository.Save();
            return RedirectToAction(actionName: nameof(ShowProfileInformation),controllerName: "Account");
        }

        [HttpGet]
        public Microsoft.AspNetCore.Mvc.IActionResult EditProfile(System.Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.GetUserById(id.Value);
            ViewModels.ProfileViewModel viewModel = new ViewModels.ProfileViewModel()
            {
               Id=user.Id,
               Username=user.Username,
               EMail=user.EMail,

               FullName=user.FullName,
               Country=user.Country,
               State=user.State,

               City=user.City,
               Address=user.Address,
               ZipCode=user.ZipCode,

               IsActive=user.IsActive,
               PhoneNumber=user.PhoneNumber,
               RoleId=user.RoleId,
               ImageName=user.ImageName,

        };
            if (user == null)
            {
                return NotFound();
            }

            //var roles =await roleManager.Roles.ToListAsync();
            //ViewData["RoleId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(roles, "Id", "Name", user.RoleId);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Microsoft.AspNetCore.Mvc.IActionResult EditProfile(System.Guid id, ViewModels.ProfileViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }
            var orginalUser = UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.GetById(viewModel.Id);
            
            if (ModelState.IsValid )
            {
                try
                {

                    orginalUser.Id = viewModel.Id;
                    orginalUser.Username = viewModel.Username;             
                    orginalUser.FullName = viewModel.FullName;
                    orginalUser.EMail = viewModel.EMail;
                    orginalUser.Address = viewModel.Address;
                    orginalUser.City = viewModel.City;
                    orginalUser.Country = viewModel.Country;
                    orginalUser.PhoneNumber = viewModel.PhoneNumber;
                    orginalUser.ZipCode = viewModel.ZipCode;
                    //orginalUser.RoleId = viewModel.RoleId;
                    orginalUser.State = viewModel.State;
                    orginalUser.IsActive = viewModel.IsActive;

                    if (viewModel.ImageUpload != null)
                    {
                        if (orginalUser.ImageName != null)
                        {
                            var oldfilePath = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot", "Images"))
                                                                     .Root + $@"\{orginalUser.ImageName}";
                            System.IO.File.Delete(oldfilePath);
                        }
                        orginalUser.ImageName = $"{orginalUser.Id}{System.IO.Path.GetExtension(viewModel.ImageUpload.FileName)}";

                        var newfilePath = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot", "Images"))
                                                                     .Root + $@"\{orginalUser.ImageName}";

                        using (System.IO.FileStream fileStream = System.IO.File.Create(newfilePath))
                        {
                            viewModel.ImageUpload.CopyTo(fileStream);
                            fileStream.Flush();
                        }
                    }
                    UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.Update(orginalUser);
                    UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.IsExist(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ShowProfileInformation), "Account");
            }
            //var roles =await roleManager.Roles.ToListAsync();
            //ViewData["RoleId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(roles, "Id", "Name", viewModel.RoleId);
            return View(viewModel);
        }

        //********************************************???Need to implement????**********************************
        //[HttpGet]
        //public IActionResult ChangeAccountPassword(System.Guid id)
        //{
        //    if (id==null)
        //    {
        //        return NotFound();
        //    }
           
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ChangeAccountPassword(ViewModels.PasswordChangeViewModel viewModel)
        //{
        //    var theUser = UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.GetUserByUsername(viewModel.Username);
        //    if (ModelState.IsValid && theUser!=null )
        //    {
        //        if (theUser.Password == viewModel.OldPassword)
        //        {
        //            if (theUser.Username==User.Identity.Name)
        //            {
        //                var tempidentity = new IdentityUser()
        //                {
        //                    UserName=theUser.Username,
        //                    Email=theUser.EMail,
        //                    EmailConfirmed=true,
        //                };
        //                var result1 = await userManager.RemovePasswordAsync(tempidentity);
        //                if (result1.Succeeded)
        //                {
        //                    theUser.Password = viewModel.NewPassword;
        //                    UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.Update(theUser);
        //                    UnitOfWork.BethanyPieShopUnitOfWork.UserRepository.Save();
        //                    var result = await userManager.AddPasswordAsync(tempidentity, viewModel.ConfirmNewPassword);
        //                    if (result.Succeeded)
        //                    {
        //                        return RedirectToAction(nameof(ShowProfileInformation), "Account");
        //                    }
                            
        //                }
        //            }
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    return View();
        //}

    }
}
