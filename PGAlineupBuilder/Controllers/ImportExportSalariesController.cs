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
using Microsoft.Net.Http.Headers;



// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PGAlineupBuilder.Controllers
{
    public class ImportExportSalariesController : Controller
    {
        private PGAlineupBuilderDbContext context;

       // public ImportExportSalariesController(PGAlineupBuilderDbContext dbContext)
        //{
           // context = dbContext;
        //}

        private readonly IHostingEnvironment _environment;

        public ImportExportSalariesController(IHostingEnvironment environment, PGAlineupBuilderDbContext dbContext)
        {
            _environment = environment;

            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UploadDKcsv()
        {
           // UploadDKcsvViewModel uploadDKcsvViewModel = new UploadDKcsvViewModel();
            return View("UploadDKcsv");

         }

        // [HttpPost]
        // public async Task<IActionResult> DKimport(UploadDKcsvViewModel uploadDKcsvViewModel)
        //{
        //if (ModelState.IsValid)
        // {
        // var salary = new DKsalarys
        //   {
        //  Name = uploadDKcsvViewModel.Name

        // };
        //  using (var memoryStream = new MemoryStream())
        // {
        // await uploadDKcsvViewModel.csvUpload.CopyToAsync(memoryStream);
        //  salary.csvUpload = memoryStream.ToArray();
        // }

        //context.DKS.Add(salary);
        // context.SaveChanges();
        // return Redirect("/Index");

        //}

        //return View("/UploadDKcsv");

        //}


        [HttpPost]
        public IActionResult DKimport(ICollection<IFormFile> DKfiles)
        {
            var DKuploads = Path.Combine(_environment.WebRootPath, "DKuploads");

            // var DKuploads = Path.GetTempFileName();

            long size = 0;

            foreach(var file in DKfiles)
            {
                if (file.Length > 0)
               {
                    var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    //filename = _environment.WebRootPath + $@"\{filename}";
                    size += file.Length;

                    using (FileStream fstream = new FileStream(Path.Combine(DKuploads, filename), FileMode.Create))
                    {
                        file.CopyTo(fstream);
                        fstream.Flush();
                    }

                    ViewBag.Message = $"{DKfiles.Count} file(s) / {size} bytes uploaded sucessfully!";
                    return View("UploadDKcsv");

                        // using (var fileStream = new FileStream(Path.Combine(DKuploads, file.FileName), FileMode.Create))
                        // {
                        //await file.CopyToAsync(fileStream);
                  //  }
                }
                else
                {
                    return View("UploadDKcsv");
                }
                
            }

            return View("Index");
        }
    }
}
