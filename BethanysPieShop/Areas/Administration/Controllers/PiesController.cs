//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using Models;

namespace BethanysPieShop.Areas.Administration.Controllers
{
    
    [Microsoft.AspNetCore.Mvc.Area("Administration")]
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin,Boss")]
    public class PiesController : Infrastracture.BaseController
    {       

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public  Microsoft.AspNetCore.Mvc.IActionResult Index()
        {
            var pies = UnitOfWork.BethanyPieShopUnitOfWork.PieRepository.Get(includeProperties:"PieCatagory");
            return View(model:pies);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous()]
        public Microsoft.AspNetCore.Mvc.IActionResult ListByCategory(string categoryName)
        {
            ViewModels.PiesListViewModel viewModel = new ViewModels.PiesListViewModel();
            if (string.IsNullOrEmpty(categoryName))
            {
                viewModel.piesListByCategory = UnitOfWork.BethanyPieShopUnitOfWork.PieRepository.Get(includeProperties: "PieCatagory");
                viewModel.CurrentPieCategory = Resources.DataDictionary.AllPies;
            }
            else
            {
                var pies = UnitOfWork.BethanyPieShopUnitOfWork.PieRepository.GetPiesByCatagory(categoryName);
                viewModel.piesListByCategory = pies;
                viewModel.CurrentPieCategory = categoryName;
            }
            
                    
            return View(model: viewModel);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public Microsoft.AspNetCore.Mvc.IActionResult Create( )
        {
            var PieCatagorySelectList = UnitOfWork.BethanyPieShopUnitOfWork.CatagoryRepository.Get();
            ViewData["CatagoryId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(PieCatagorySelectList, "Id", "CatagoryName");
            return View();
        }  

        
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public Microsoft.AspNetCore.Mvc.IActionResult Create( ViewModels.PieViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Models.Pie newPie = new Models.Pie()
                { 
                    Name=viewModel.Name,
                    ShortDescription=viewModel.ShortDescription,
                    LongDescription=viewModel.LongDescription,
                    AllergyInformation=viewModel.AllergyInformation,
                    Price=viewModel.Price,
                    CatagoryId=viewModel.CatagoryId,
                    IsPieOfTheWeek=viewModel.IsPieOfTheWeek,
                    InStock=viewModel.InStock,
                };

                if (viewModel.ImageUpload!=null)
                {
                    newPie.ImageName = $"{newPie.Id}{System.IO.Path.GetExtension(viewModel.ImageUpload.FileName)}";
                    var filePath=new Microsoft.Extensions.FileProviders.PhysicalFileProvider(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot", "Images"))
                                                                       .Root + $@"\{newPie.ImageName}";

                    using (System.IO.FileStream fileStream=System.IO.File.Create(filePath))
                    {
                        viewModel.ImageUpload.CopyTo(fileStream);
                        fileStream.Flush();
                    }

                }
                UnitOfWork.BethanyPieShopUnitOfWork.PieRepository.Insert(newPie);
                UnitOfWork.BethanyPieShopUnitOfWork.PieRepository.Save();
                return RedirectToAction(actionName: "Index", controllerName: "Pies",routeValues:new { Areas= "Administration " });
            }
            var PieCatagorySelectList = UnitOfWork.BethanyPieShopUnitOfWork.CatagoryRepository.Get();
            ViewData["CatagoryId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(PieCatagorySelectList, Resources.DataDictionary.ID, "CatagoryName","");
            return View(model:viewModel);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public Microsoft.AspNetCore.Mvc.IActionResult Edit(System.Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pie = UnitOfWork.BethanyPieShopUnitOfWork.PieRepository.GetById(id.Value);
            ViewModels.PieViewModel viewModel=new ViewModels.PieViewModel() 
            {
                Id=pie.Id,
                Name=pie.Name,
                ShortDescription=pie.ShortDescription,
                LongDescription=pie.LongDescription,
                AllergyInformation=pie.AllergyInformation,
                CatagoryId=pie.CatagoryId,
                Price=pie.Price,
                IsPieOfTheWeek=pie.IsPieOfTheWeek,
                InStock=pie.InStock,
                ImageName=pie.ImageName
            };
            if (pie == null)
            {
                return NotFound();
            }
            var PieCatagorySelectList = UnitOfWork.BethanyPieShopUnitOfWork.CatagoryRepository.Get();
            ViewData["CatagoryId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(PieCatagorySelectList, Resources.DataDictionary.ID, "CatagoryName",pie.CatagoryId);
            return View(model:viewModel);
        }

       
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public Microsoft.AspNetCore.Mvc.IActionResult Edit(System.Guid id, ViewModels.PieViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var orginalPie = UnitOfWork.BethanyPieShopUnitOfWork.PieRepository.GetById(viewModel.Id);
                    orginalPie.Id = viewModel.Id;
                    orginalPie.Name = viewModel.Name;
                    orginalPie.ShortDescription = viewModel.ShortDescription;
                    orginalPie.LongDescription = viewModel.LongDescription;
                    orginalPie.AllergyInformation = viewModel.AllergyInformation;
                    orginalPie.CatagoryId = viewModel.CatagoryId;
                    orginalPie.Price = viewModel.Price;
                    orginalPie.IsPieOfTheWeek = viewModel.IsPieOfTheWeek;
                    orginalPie.InStock = viewModel.InStock;
                    //orginalPie.ImageName = viewModel.ImageName; oops!BeCarfull we should not initilize it here

                    
                    if (viewModel.ImageUpload!=null)
                    {
                        if (viewModel.ImageName != null)
                        {
                            var oldfilePath = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot", "Images"))
                                                                     .Root + $@"\{orginalPie.ImageName}";
                            System.IO.File.Delete(oldfilePath);
                        }
                        orginalPie.ImageName = $"{orginalPie.Id}{System.IO.Path.GetExtension(viewModel.ImageUpload.FileName)}";

                        var newfilePath = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot", "Images"))
                                                                     .Root + $@"\{orginalPie.ImageName}";

                        using (System.IO.FileStream fileStream = System.IO.File.Create(newfilePath))
                        {
                            viewModel.ImageUpload.CopyTo(fileStream);
                            fileStream.Flush();
                        }
                    }
                    UnitOfWork.BethanyPieShopUnitOfWork.PieRepository.Update(orginalPie);
                    UnitOfWork.BethanyPieShopUnitOfWork.PieRepository.Save();
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
                {
                    if (UnitOfWork.BethanyPieShopUnitOfWork.PieRepository.IsExist(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(actionName: "Index", controllerName: "Pies", routeValues: new { Areas = "Administration " });
            }

            var PieCatagorySelectList = UnitOfWork.BethanyPieShopUnitOfWork.CatagoryRepository.Get();
            ViewData["CatagoryId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(PieCatagorySelectList, Resources.DataDictionary.ID, "CatagoryName", viewModel.CatagoryId);
            return View(viewModel);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public Microsoft.AspNetCore.Mvc.IActionResult Delete(System.Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pie =UnitOfWork.BethanyPieShopUnitOfWork.PieRepository.GetById(id.Value);
            if (pie == null)
            {
                return NotFound();
            }

            return View(pie);
        }

       
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ActionName("Delete")]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public Microsoft.AspNetCore.Mvc.IActionResult DeleteConfirmed(System.Guid id)
        {
            var pie = UnitOfWork.BethanyPieShopUnitOfWork.PieRepository.GetById(id);
            if (pie.ImageName!=null)
            {
                var filePath = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot", "Images"))
                                                                     .Root + $@"\{pie.ImageName}";
                System.IO.File.Delete(filePath);
            }
            UnitOfWork.BethanyPieShopUnitOfWork.PieRepository.Delete(pie);
            UnitOfWork.BethanyPieShopUnitOfWork.PieRepository.Save();
            return RedirectToAction(nameof(Index));
        }

      
    }
}
