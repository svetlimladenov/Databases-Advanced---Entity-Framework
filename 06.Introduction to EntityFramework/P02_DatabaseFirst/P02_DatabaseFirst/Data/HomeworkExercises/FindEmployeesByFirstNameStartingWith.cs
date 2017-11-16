using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P02_DatabaseFirst.Data.HomeworkExercises
{
    public class FindEmployeesByFirstNameStartingWith
    {
        public FindEmployeesByFirstNameStartingWith()
        {
            
        }

        public void Run()
        {
            using (var db = new SoftUniContext())
            {
                var employees = db.Employees
                    .Where(e => e.FirstName.StartsWith("Sa"))
                    .Select(e => new
                    {
                        Name = $"{e.FirstName} {e.LastName}",
                        e.JobTitle,
                        e.Salary,
                        e.FirstName,
                        e.LastName
                    })
                    .ToList();

                foreach (var employee in employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName))
                {
                    Console.WriteLine($"{employee.Name} - {employee.JobTitle} - (${employee.Salary:F2})");
                }
            }
        }
    }
}
