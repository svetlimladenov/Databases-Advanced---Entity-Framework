using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P02_DatabaseFirst.Data.HomeworkExercises
{
    public class Employee_147
    {
        public Employee_147()
        {
            
        }

        public void Run()
        {
            using (var db = new SoftUniContext())
            {
                var employee147 = db.Employees
                    .Where(e => e.EmployeeId == 147)
                    .Select(e => new
                    {
                        Name = $"{e.FirstName} {e.LastName}",
                        JobTitle = e.JobTitle,
                        Projects = e.EmployeeProjects.Select(ep => new
                        {
                            ep.Project.Name
                        })
                    })
                    .ToList();

                foreach (var emp in employee147)
                {
                    Console.WriteLine($"{emp.Name} - {emp.JobTitle}");
                    foreach (var project in emp.Projects.OrderBy(ep => ep.Name))
                    {
                        Console.WriteLine(project.Name);
                    }
                }
            }
        }
    }
}
