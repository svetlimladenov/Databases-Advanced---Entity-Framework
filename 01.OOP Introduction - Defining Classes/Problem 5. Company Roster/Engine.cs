using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_5.Company_Roster
{
    public class Engine
    {
        public Engine()
        {
            
        }

        public void Run()
        {
            var allEmployees = new Dictionary<string,Employee>();
            var n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                var inputArgs = Console.ReadLine().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                var name = inputArgs[0];
                var salary = double.Parse(inputArgs[1]);
                var position = inputArgs[2];
                var department = inputArgs[3];
                if (inputArgs.Length == 5)
                {
                    int age;
                    if (int.TryParse(inputArgs[4],out age))
                    {
                        allEmployees.Add(name,new Employee(name,salary,position,department, "n/a", age));
                    }
                    else
                    {
                        allEmployees.Add(name, new Employee(name, salary, position, department, inputArgs[4], -1));
                    }
                }
                else if(inputArgs.Length == 6)
                {
                    allEmployees.Add(name, new Employee(name, salary, position, department, inputArgs[4], int.Parse(inputArgs[5])));
                }
                else
                {
                    allEmployees.Add(name, new Employee(name, salary, position, department, "n/a", -1));
                }
            }
            var allDepartmentsWithSalary = new Dictionary<string,double>();
            foreach (var employee in allEmployees)
            {
                if (!allDepartmentsWithSalary.ContainsKey(employee.Value.Department))
                {
                    allDepartmentsWithSalary[employee.Value.Department] = 0.0;
                }

                allDepartmentsWithSalary[employee.Value.Department] += employee.Value.Salary;
            }

            allDepartmentsWithSalary = allDepartmentsWithSalary
                .OrderByDescending(d => d.Value)
                .ToDictionary(d => d.Key, d => d.Value);
            foreach (var department in allDepartmentsWithSalary)
            {
                Console.WriteLine($"Highest Average Salary: {department.Key}");
                foreach (var employee in allEmployees.Where(e => e.Value.Department == department.Key).OrderByDescending(e => e.Value.Salary))
                {
                    Console.WriteLine($"{employee.Value.Name} {employee.Value.Salary:F2} {employee.Value.Email} {employee.Value.Age}");
                }
                break;
            }
        }
    }
}
