using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using P02_DatabaseFirst.Data;

namespace P02_DatabaseFirst
{
    public class EmployeesAndProjects
    {
        public EmployeesAndProjects()
        {
            
        }

        public void Run()
        {
            using (var db = new SoftUniContext())
            {
                var employees = db.Employees
                    .Where(
                        e => e.EmployeeProjects.Any(ep =>
                            ep.Project.StartDate.Year >= 2001 &&
                            ep.Project.StartDate.Year <= 2003
                        ))
                    .Select(e => new
                    {
                        Name = $"{e.FirstName} {e.LastName}",
                        ManagerName = $"{e.Manager.FirstName} {e.Manager.LastName}",
                        Projects = e.EmployeeProjects.Select(ep => new
                        {
                            ep.Project.Name,
                            ep.Project.StartDate,
                            ep.Project.EndDate
                        })
                    })
                    .Take(30)
                    .ToList();

                foreach (var emp in employees)
                {
                    Console.WriteLine($"{emp.Name} – Manager: {emp.ManagerName}");
                    foreach (var empProject in emp.Projects)
                    {

                        if (empProject.EndDate == null)
                        {
                            Console.WriteLine($"--{empProject.Name} - {empProject.StartDate} - not finished");
                        }
                        else
                        {
                            Console.WriteLine($"--{empProject.Name} - {empProject.StartDate} - {empProject.EndDate}");
                        }
                    }
                }
            }
        }
    }
}
