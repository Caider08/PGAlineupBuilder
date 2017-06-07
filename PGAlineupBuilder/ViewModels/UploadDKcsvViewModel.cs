using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PGAlineupBuilder.Models;
using System.IO;

namespace PGAlineupBuilder.ViewModels
{
    public class UploadDKcsvViewModel
    {
        public string Name { get; set; }

        public IFormFile csvUpload { get; set; }



        public UploadDKcsvViewModel()
        {

        }
            



    }
}
