using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;
using Restaurant.ViewModel;

namespace Restaurant.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment  _hostingEnvironment;

        public EmployeeController(IEmployeeRepository employeeRepository,
                                  IWebHostEnvironment hostingEnvironment)
        {
            this._employeeRepository = employeeRepository;
            this._hostingEnvironment = hostingEnvironment;
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
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                // If the Photo property on the incoming model is not null then the user has selected an image to upload
                if (model.Photo != null)
                {
                    string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                Employee employee = new Employee()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };
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