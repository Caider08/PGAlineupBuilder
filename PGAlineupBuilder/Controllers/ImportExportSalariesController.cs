using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using PGAlineupBuilder.ViewModels;
using PGAlineupBuilder.Models;
using PGAlineupBuilder.Data;



// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PGAlineupBuilder.Controllers
{
    public class ImportExportSalariesController : Controller
    {
        private PGAlineupBuilderDbContext context;

        public ImportExportSalariesController(PGAlineupBuilderDbContext dbContext)
        {
            context = dbContext;
        }

        //private IHostingEnvironment _environment;

        //public ImportExportSalariesController(IHostingEnvironment environment)
        //{
        //    _environment = environment;
        //}

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UploadDKcsv()
        {
            UploadDKcsvViewModel uploadDKcsvViewModel = new UploadDKcsvViewModel();
            return View(uploadDKcsvViewModel);
       
        }

        [HttpPost]
        public async Task<IActionResult> DKimport(UploadDKcsvViewModel uploadDKcsvViewModel)
        {
            if (ModelState.IsValid)
            {
                var salary = new DKcsvUpload
                {
                    Name = uploadDKcsvViewModel.Name,

                };
                using (var memoryStream = new MemoryStream())
                {
                    await uploadDKcsvViewModel.csvUpload.CopyToAsync(memoryStream);
                    salary.csvUpload = memoryStream.ToArray();
                }

                context.DKup.Add(salary);
                return Redirect("/Index");

            }

            return View("/UploadDKcsv");
           
        }



        //[HttpPost]
        //public async Task<IActionResult> DKimport(ICollection<IFormFile> DKfiles)
        //{
        //    var DKuploads = Path.Combine(_environment.WebRootPath, "DKuploads");
        //    foreach(var file in DKfiles)
        //    {
        //        if (file.Length > 0)
        //        {
        //            using (var fileStream = new FileStream(Path.Combine(DKuploads, file.FileName), FileMode.Create))
        //            {
        //                await file.CopyToAsync(fileStream);
        //            }
        //        }
        //    }
        //    return View("Index");
        //}
    }
}
