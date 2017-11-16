using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using P02_DatabaseFirst.Data;

namespace P02_DatabaseFirst
{
    public class AddressesByTown
    {
        public AddressesByTown()
        {
            
        }

        public void Run()
        {
            using (var db = new SoftUniContext())
            {
                var addresses = db.Addresses
                    .Select(a => new
                    {
                        AddressName = a.AddressText,
                        TownName = a.Town.Name,
                        EmployeesCount = a.Employees.Count
                    })
                    .ToList();

                foreach (var address in addresses.OrderByDescending(a => a.EmployeesCount).ThenBy(a => a.TownName).ThenBy(a => a.AddressName).Take(10))
                {
                    Console.WriteLine($"{address.AddressName}, {address.TownName} - {address.EmployeesCount} employees");
                }
            }
        }
    }
}
