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
using System.Net;




// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PGAlineupBuilder.Controllers
{
    public class ImportExportSalariesController : Controller
    {
        private PGAlineupBuilderDbContext context;

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
            return View("UploadDKcsv");

        }

        public IActionResult UploadFDcsv()
        {
            return View("UploadFANDUELcsv");

        }


        //Takes files from UploadDKcsv file and writes them to DKuploads Directory and passes string "UploadName" to DKcreate Method
        [HttpPost]
        public async Task<IActionResult> DKimport(ICollection<IFormFile> DKfiles, string uploadName)
        {

            long size = 0;

            foreach (var file in DKfiles)
            {
                if (file.Length > 0)
                {
                    if (!string.IsNullOrEmpty(uploadName))
                    {
                        var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                        var DKuploads = System.IO.Path.Combine(_environment.ContentRootPath, "DKuploads");

                        string UPLOADname = uploadName;


                        using (FileStream fstream = new FileStream(Path.Combine(DKuploads, UPLOADname), FileMode.Create))
                        {

                            await file.CopyToAsync(fstream);

                            fstream.Flush();
                        }

                        size += file.Length;

                        return RedirectToAction("DKcreate", "ImportExportSalaries", new { Uname = $"{UPLOADname}" });

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

        //Method takes a string and uses the string to reference the file in DKuploads to pull a DkTourney and its golfers from...Then creates them and pushes to Database.
        public IActionResult DKcreate(string Uname)
        {

            if (!string.IsNullOrWhiteSpace(Uname))
            {
                List<Golfer> theseGolfers = new List<Golfer>();

                theseGolfers = PGAuploads.WeeksFDGolfers(Uname);

                string GameInfo = PGAuploads.WeeksGameInfo(Uname);

                //check to see if this Tournament has already been pushed to the Database.
                var isDuplicate = context.DKT.Any(a => a.Name == GameInfo);

                if (isDuplicate)
                {
                    ViewBag.Message = "You've already uploaded this tournament";
                    return View("UploadDKcsv");
                }
                else
                {   //PUSH created Golfers and DkTourney to the Database
                    foreach (Golfer golfer in theseGolfers)
                    {
                        context.GOLFER.Add(golfer);

                    }

                    DkTourney DKtourney = new DkTourney(theseGolfers)
                    {
                        Name = GameInfo,

                    };

                    context.DKT.Add(DKtourney);
                    context.SaveChanges();

                    ViewBag.Game = GameInfo;
                    ViewBag.Golfers = theseGolfers;
                    return View("SalariesCreated");
                }


            }

            ViewBag.Message = "Upload a file please";
            return View("UploadDKcsv");


        }
        [HttpPost]
        public async Task<IActionResult> FDimport(ICollection<IFormFile> FDfiles, string uploadName)
        {

            long size = 0;

            foreach (var file in FDfiles)
            {
                if (file.Length > 0)
                {
                    if (!string.IsNullOrEmpty(uploadName))
                    {
                        var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                        var FDuploads = System.IO.Path.Combine(_environment.ContentRootPath, "FDuploads");

                        string UPLOADname = uploadName;


                        using (FileStream fstream = new FileStream(Path.Combine(FDuploads, UPLOADname), FileMode.Create))
                        {

                            await file.CopyToAsync(fstream);

                            fstream.Flush();
                        }

                        size += file.Length;

                        return RedirectToAction("FDcreate", "ImportExportSalaries", new { Uname = $"{UPLOADname}" });

                    }
                    else
                    {
                        ViewBag.Message = "Name your file please";
                        return View("UploadFANDUELcsv");
                    }

                }


            }

            ViewBag.Message = "Select a file please";
            return View("UploadFANDUELcsv");




        }

        public IActionResult FDcreate(string Uname)
        {

            if (!string.IsNullOrWhiteSpace(Uname))
            {
                List<FDgolfer> theseGolfers = new List<FDgolfer>();

                theseGolfers = PGAuploads.WeeksFDgolfers(Uname);

                int year = int.Parse(DateTime.Now.ToString("yyyy"));

                string GameInfo = Uname + $" {year}";

                //check to see if this Tournament has already been pushed to the Database.
                var isDuplicate = context.FDT.Any(a => a.Name == GameInfo);

                if (isDuplicate)
                {
                    ViewBag.Message = "You've already uploaded this tournament";
                    return View("UploadFANDUELcsv");
                }
                else
                {   //PUSH created Golfers and DkTourney to the Database
                    foreach (FDgolfer golfer in theseGolfers)
                    {
                        context.FDGOLFER.Add(golfer);

                    }

                    FDtourney fdTourney = new FDtourney(theseGolfers)
                    {
                        Name = GameInfo,

                    };

                    context.FDT.Add(fdTourney);
                    context.SaveChanges();

                    ViewBag.Game = GameInfo;
                    ViewBag.Golfers = theseGolfers;
                    return View("FDSalariesCreated");
                }


            }

            ViewBag.Message = "Upload a file please";
            return View("UploadDKcsv");
        }
    }


}
  

        


