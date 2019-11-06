using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "Ram",
                    Email = "ram@restaurant.com",
                    Department = Department.Cooking,
                },
                new Employee
                {
                    Id = 2,
                    Name = "Cherry",
                    Email = "cherry@restaurant.com",
                    Department = Department.Serving,
                },
                new Employee
                {
                    Id = 3,
                    Name = "teju",
                    Email = "teju@restaurant.com",
                    Department = Department.Accounting,
                }
                );
        }
    }
}
