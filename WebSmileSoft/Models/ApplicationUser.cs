using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;

namespace WebSmileSoft.Models
{
    
 public class ApplicationUser : IdentityUser
        {
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public DateTime RegistrationDate { get; set; }
            public string? ProfileImage { get; set; }
            public string? Address { get; set; }
            // Otras propiedades personalizadas aquí...
        }
    }

