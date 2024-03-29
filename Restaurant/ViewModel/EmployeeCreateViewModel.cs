﻿using Microsoft.AspNetCore.Http;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.ViewModel
{
    public class EmployeeCreateViewModel
    {
        public int Id { get; set; }

        [Required, MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        ErrorMessage = "Invalid email format")]
        [Display(Name = "Office Mail")]
        public string Email { get; set; }

        [Required]
        public Department? Department { get; set; }

        public IFormFile Photo { get; set; }
    }
}
