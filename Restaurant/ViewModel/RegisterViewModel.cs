using System;
﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.CustomValidationAttributes;

namespace Restaurant.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        [ValidEmailDomain(allowedDomain:"@restaurant.com", 
            ErrorMessage = "Domain must be restaurant.com")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        [Compare("Password", ErrorMessage ="Password and Confirmation Password does not match")]
        public string ConfirmPassword { get; set; }

    }
}
