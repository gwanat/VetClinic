using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace VetClinic.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the IdentityUser class
    public class ApplicationUser : IdentityUser
    {
        // Additional properties for the ApplicationUser class can be added here.
        // For example:
        // public string FirstName { get; set; }
        // public string LastName { get; set; }
    }
}

