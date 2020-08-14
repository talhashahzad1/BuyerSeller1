using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using BuyerSeller.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using BuyerSeller.ViewModel;
using BuyerSeller.Models;

namespace BuyerSeller.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IHostingEnvironment webHostEnvironment;

        public ServiceController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment )
          {
            dbContext = context;
            webHostEnvironment = hostingEnvironment;

        }
        public async Task<IActionResult> Index()
        {
            var sellerServices = await dbContext.Services.ToListAsync();
            return View(sellerServices);
        }

        public IActionResult CreateService()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateService(ServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                Service buyerservice = new Service
                {
                    Title = model.Title,
                    Description = model.Description,
                    tags = model.tags,
                    Picture = uniqueFileName,
                };
                dbContext.Add(buyerservice);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        private string UploadedFile(ServiceViewModel model)
        {
            string uniqueFileName = null;

            if (model.Picture != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Pictures");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Picture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Picture.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }



}