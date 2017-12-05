using System;
using System.Linq;
using Demo.Data;
using Demo.Data.Models;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Demo
{
     public class Program
    {
        public static void Main()
        {
            var context = new EmployeesDbContext();
            var context2 = new EmployeesDbContext();

            var firstEmployee = context.Employees.Find(1);
            var secondEmployee = context2.Employees.Find(1);

            firstEmployee.Salary = 10000;
            secondEmployee.Salary = 2000;

            context.SaveChanges();
            context2.SaveChanges();
            
            
        }
    }
}
