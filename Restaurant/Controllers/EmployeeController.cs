using System;
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

        public ViewResult Index()
        {
            var model = _employeeRepository.GetEmployees();
            return View(model);
        }
        
        public ViewResult Details(int id)
        {
            id = id == 0 ? 1 : id;
            EmployeeDetailsViewModel viewModel = new EmployeeDetailsViewModel()
            {
                Employee = _employeeRepository.GetEmployee(id),
                PageTitle = "Employee Details"
            };
            return View(viewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee = _employeeRepository.Create(employee);
                return RedirectToAction("details", new { id = employee.Id });
            }
            return View();
        }

        // ObjectResult looks at the Request Accept Header and if it is set to application/xml, then XML data is returned.
        // If the Accept header is set to application/json, then JSON data is returned
        public ObjectResult Get(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            return new ObjectResult(employee);
        }

    }
}