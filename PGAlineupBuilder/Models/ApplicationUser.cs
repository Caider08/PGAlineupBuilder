using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PGAlineupBuilder.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }

    public class Admins
    {
        public int ID { get; set; }

        public string IdentyID { get; set; }

        public ApplicationUser Identity { get; set; }

        public string Location { get; set; }

    }
}
