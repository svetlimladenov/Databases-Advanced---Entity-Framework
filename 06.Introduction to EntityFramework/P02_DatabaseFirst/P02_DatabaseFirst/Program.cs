using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.HomeworkExercises;
using P02_DatabaseFirst.Data.Models;
using P02_DatabaseFirst.ViewModels;

namespace P02_DatabaseFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            //var findEmpStartingWith = new FindEmployeesByFirstNameStartingWith();
            //findEmpStartingWith.Run();

            //var increaseSalaries = new Increase_Salaries();
            //increaseSalaries.Run();

            //var last10Projects = new Find_Latest_10_Projects();
            //last10Projects.Run();

            //var depWith5 = new Departments_with_More_Than_5_Employees();
            //depWith5.Run();

            //var emp147 = new Employee_147();
            //emp147.Run();

            //var addreesesByTown = new AddressesByTown();
            //addreesesByTown.Run();

            //var emp = new EmployeesAndProjects();
            //emp.Run();

            var context = new SoftUniContext();

            var townEmployeesCount = context
                .Employees
                .GroupBy(e => e.Address.Town.Name)
                .Select(g => new TownViewModel(g.Key,g.Count()))             
                .OrderByDescending(t => t.ResidentCount)
                .ToArray();

            var employeesProfileView = context
                .Employees
                .Select(e => new EmployeeProfileViewModel(e))
                .ToList();  
            foreach (var epv in employeesProfileView)
            {
                Console.WriteLine(epv.ToString());
            }
        }
    }
}
