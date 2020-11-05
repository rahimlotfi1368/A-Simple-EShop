using System;
using System.Collections.Generic;
using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using Models;
namespace BethanysPieShop.Areas.Administration.Controllers
{
    [Microsoft.AspNetCore.Mvc.Area("Administration")]
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin,Boss")]
    public class CatagoriesController : Infrastracture.BaseController
    {
        public CatagoriesController():base()
        {

        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public  Microsoft.AspNetCore.Mvc.IActionResult Index()
        {
            var CatagoryList= UnitOfWork.BethanyPieShopUnitOfWork.CatagoryRepository.Get();
            return View(model:CatagoryList);
        }


        [Microsoft.AspNetCore.Mvc.HttpGet]
        public Microsoft.AspNetCore.Mvc.IActionResult Create()
        {
            return View();
        }

        
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public  Microsoft.AspNetCore.Mvc.IActionResult Create(ViewModels.CatagoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var newCatagory = new Models.Catagory()
                {
                    CatagoryName=viewModel.CatagoryName,
                    Description=viewModel.Description,
                };
                UnitOfWork.BethanyPieShopUnitOfWork.CatagoryRepository.Insert(newCatagory);
                UnitOfWork.BethanyPieShopUnitOfWork.CatagoryRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public Microsoft.AspNetCore.Mvc.IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catagory= UnitOfWork.BethanyPieShopUnitOfWork.CatagoryRepository.GetById(id.Value);
            ViewModels.CatagoryViewModel viewModel = new ViewModels.CatagoryViewModel() 
            {
                Id=catagory.Id,
                CatagoryName=catagory.CatagoryName,
                Description=catagory.Description
            };
            if (catagory == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }
               
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public Microsoft.AspNetCore.Mvc.IActionResult Edit(Guid id, ViewModels.CatagoryViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var orginalCatagory = UnitOfWork.BethanyPieShopUnitOfWork.CatagoryRepository.GetById(id);
                    orginalCatagory.CatagoryName = viewModel.CatagoryName;
                    orginalCatagory.Description = viewModel.Description;
                    UnitOfWork.BethanyPieShopUnitOfWork.CatagoryRepository.Update(orginalCatagory);
                    UnitOfWork.BethanyPieShopUnitOfWork.CatagoryRepository.Save();

                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
                {
                    if (!UnitOfWork.BethanyPieShopUnitOfWork.CatagoryRepository.IsExist(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public  Microsoft.AspNetCore.Mvc.IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catagory = UnitOfWork.BethanyPieShopUnitOfWork.CatagoryRepository.GetById(id.Value);
            if (catagory == null)
            {
                return NotFound();
            }

            return View(catagory);
        }

       
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ActionName("Delete")]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public Microsoft.AspNetCore.Mvc.IActionResult DeleteConfirmed(Guid id)
        {
            UnitOfWork.BethanyPieShopUnitOfWork.CatagoryRepository.DeleteById(id);
            UnitOfWork.BethanyPieShopUnitOfWork.CatagoryRepository.Save();
            return RedirectToAction(nameof(Index));
        }

    }
}
