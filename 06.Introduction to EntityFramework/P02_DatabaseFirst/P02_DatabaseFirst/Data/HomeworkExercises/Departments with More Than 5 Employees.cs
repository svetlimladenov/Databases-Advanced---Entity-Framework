using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P02_DatabaseFirst.Data.HomeworkExercises
{
    public class Departments_with_More_Than_5_Employees
    {
        public Departments_with_More_Than_5_Employees()
        {
            
        }

        public void Run()
        {
            using (var db = new SoftUniContext())
            {
                var departments = db.Departments
                    .Where(d => d.Employees.Count > 5)
                    .Select(d => new
                    {
                        departmentName = d.Name,
                        managerName = $"{d.Manager.FirstName} {d.Manager.LastName}",
                        employees = d.Employees.Select(e => new
                        {
                            empName = $"{e.FirstName} {e.LastName}",
                            e.FirstName,
                            e.LastName,
                            empJobTitle = e.JobTitle
                        })
                    })
                    .ToList();

                foreach (var department in departments.OrderBy(e => e.employees.Count()).ThenBy(d => d.departmentName))
                {
                    Console.WriteLine($"{department.departmentName} – {department.managerName}");
                    foreach (var employee in department.employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName))
                    {
                        Console.WriteLine($"{employee.empName} - {employee.empJobTitle}");
                    }
                    Console.WriteLine("----------");
                }
            }    
        
        }
    }
}
