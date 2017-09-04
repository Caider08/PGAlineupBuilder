using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PGAlineupBuilder.Models
{
    public class SEOsignUP
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int ID { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set;  }
    }
}
