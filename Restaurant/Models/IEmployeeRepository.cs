using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    // IEmployeeRepository interface will have all the Employee model related functionalities
    // It is creted as Interface to maintain loosely coupled code
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int Id);
        IEnumerable<Employee> GetEmployees();
    }
}
