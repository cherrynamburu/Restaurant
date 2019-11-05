using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    // This class creates mock data to model employee by inplementing the interface IEmployeeRepository
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee()
                {
                    Id = 1,
                    Name = "Cherry",
                    Email = "cherry@restaurant.com",
                    Department = Department.Cooking
                },
                new Employee()
                {
                    Id = 2,
                    Name = "Teju",
                    Email = "teju@restaurant.com",
                    Department = Department.Accounting
                },
                new Employee()
                {
                    Id = 3,
                    Name = "SaiKrishna",
                    Email = "saikrishna@restaurant.com",
                    Department = Department.Serving
                }
            };
          
        }

        public Employee Create(Employee employee)
        {
            employee.Id = _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        public Employee GetEmployee(int id)
        {
            return this._employeeList.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _employeeList;
        }
    }
}
