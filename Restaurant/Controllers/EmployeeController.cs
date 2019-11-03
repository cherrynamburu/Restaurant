﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;
using Restaurant.ViewModel;

namespace Restaurant.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        // ObjectResult looks at the Request Accept Header and if it is set to application/xml, then XML data is returned.
        // If the Accept header is set to application/json, then JSON data is returned
        public ObjectResult Get(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            return new ObjectResult(employee);
        }

        public IActionResult Details(int id)
        {
            id = id == 0 ? 1 : id;
            EmployeeDetailsViewModel viewModel = new EmployeeDetailsViewModel()
            {
                Employee = _employeeRepository.GetEmployee(id),
                PageTitle = "Employee Details"
            };
            return View(viewModel);
        }

    }
}