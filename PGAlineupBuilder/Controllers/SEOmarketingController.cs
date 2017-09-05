using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PGAlineupBuilder.Models;
using PGAlineupBuilder.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace PGAlineupBuilder.Controllers
{
    public class SEOmarketingController : Controller
    {
        private PGAlineupBuilderDbContext context;

        public SEOmarketingController(PGAlineupBuilderDbContext _context)
        {
            context = _context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(string email)
        {
            SEOsignUP signUP = new SEOsignUP()
            {
                Email = email,
            };

            context.SEO.Add(signUP);
            context.SaveChanges();
            return View("EmailSuccess", signUP);
        }

        [HttpGet]
        public IActionResult GetEmails()
        {
            IList<SEOsignUP> emails = context.SEO.ToList<SEOsignUP>();
            return View("ManageEmails", emails);
        }
    }
}
