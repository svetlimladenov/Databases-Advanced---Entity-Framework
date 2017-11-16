using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P02_DatabaseFirst.Data.HomeworkExercises
{
    public class Increase_Salaries
    {
        public Increase_Salaries()
        {
            
        }

        public void Run()
        {
            using (var db = new SoftUniContext())
            {
                var departmentsTobeIncreased = new List<string>
                {
                    "Engineering",
                    "Tool Design",
                    "Marketing",
                    "Information Services"
                };
                var toIncrease = db.Employees
                    .Where(e => departmentsTobeIncreased.Contains(e.Department.Name))
                    .ToList();

                foreach (var employee in toIncrease.OrderBy(e => e.FirstName).ThenBy(e => e.LastName))
                {
                    decimal increase = 0.12M;
                    employee.Salary += employee.Salary * increase;
                    db.SaveChanges();
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:F2})");
                }
            }
        }
    }
}
