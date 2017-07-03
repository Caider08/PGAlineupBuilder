using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PGAlineupBuilder.Data;
using PGAlineupBuilder.Models;
using Microsoft.AspNetCore.Hosting;

namespace PGAlineupBuilder.Controllers
{
    public class BuildLineupsController : Controller
    {
        private PGAlineupBuilderDbContext context;

        private readonly IHostingEnvironment _environment;

        public BuildLineupsController(IHostingEnvironment environment, PGAlineupBuilderDbContext dbContext)
        {
            environment = _environment;
            context = dbContext;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChooseDK()
        {
            var DKTourneys = context.DKT.ToList();

            return View(DKTourneys);
        }

        public IActionResult ChooseFD()
        {
            return View();
        }

        public IActionResult DisplayDK(string DKselected)
        {
            var DKSelected = context.DKT.Single(s => s.Name == DKselected);

            return View(DKSelected);
        }

    }
}
