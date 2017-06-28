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
        public async Task<IActionResult> DKimport(ICollection<IFormFile> DKfiles, string uploadName)
        {
               // Path.Combine(_environment.WebRootPath, "DKuploads");

            // var DKuploads = Path.GetTempFileName();

            long size = 0;

            foreach(var file in DKfiles)
            {
                if (file.Length > 0)
                {
                    if (!string.IsNullOrEmpty(uploadName))
                    {
                        var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        //string filename = file.FileName;
                        //string experimentPath = @"C:\Users\caide\Code\PersonalC#Projects\PGAlineupBuilder\PGAlineupBuilder\DKuploads";
                        //filename = _environment.WebRootPath + $@"\{filename}";
                        // var DKuploads = _environment.ContentRootPath + Path.DirectorySeparatorChar.ToString() + "DKuploads" + Path.DirectorySeparatorChar.ToString() + $@"\{filename}";
                        // var DKuploads = _environment.WebRootPath + Path.DirectorySeparatorChar.ToString() + "DKuploads" + Path.DirectorySeparatorChar.ToString() + $@"{filename}";
                        // var DKuploads = System.IO.Path.Combine(_environment.ContentRootPath, "DKuploads", filename);
                        var DKuploads = System.IO.Path.Combine(_environment.ContentRootPath, "DKuploads");
                        // Name = file.FileName,

                        // };
                        string UPLOADname = uploadName;


                        using (FileStream fstream = new FileStream(Path.Combine(DKuploads, UPLOADname), FileMode.Create))
                        {

                            await file.CopyToAsync(fstream);

                            fstream.Flush();
                        }
                        //using (MemoryStream memoryStream = new MemoryStream())
                        // {
                        // await file.CopyToAsync(memoryStream);
                        // byte[] uploadByteArray = memoryStream.ToArray();
                        //string uploadString = uploadByteArray.ToString();

                        // System.IO.File.WriteAllBytes(DKuploads, uploadByteArray);



                        //dkU.csvUpload = memoryStream.ToArray();

                        // }

                        // context.DKS.Add(dkU);
                        // context.SaveChanges();

                        size += file.Length;

                        ViewBag.Message = $"{DKfiles.Count} file(s) / {file.FileName}, {size} bytes uploaded sucessfully!";
                        return View("UploadDKcsv");

                        // using (var fileStream = new FileStream(Path.Combine(DKuploads, file.FileName), FileMode.Create))
                        // {
                        //;
                    }
                    else
                    {
                        ViewBag.Message = "Name your file please";
                        return View("UploadDKcsv");
                    }

                }


            }
            
            ViewBag.Message = "Select a file please";
            return View("UploadDKcsv");
        }

        [HttpPost]
        public IActionResult DKcreate(string uploadName)
        {
            if (string.IsNullOrWhiteSpace(uploadName))
            {

                return View("SalariesCreated");
            }

            ViewBag.Message = "Upload a file please";
            return View("UploadDKcsv");
        
            
        }   

    }
}
