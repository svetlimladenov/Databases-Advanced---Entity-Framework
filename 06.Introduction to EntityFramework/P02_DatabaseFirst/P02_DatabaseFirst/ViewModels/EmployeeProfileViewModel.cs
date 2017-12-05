using System;
using System.Collections.Generic;
using System.Text;
using P02_DatabaseFirst.Data.Models;

namespace P02_DatabaseFirst.ViewModels
{
    public class EmployeeProfileViewModel
    {
        public EmployeeProfileViewModel(Employee employee)
        {
            this.FirstName = employee.FirstName;
            this.LastName = employee.LastName;
            this.JobTitle = employee.JobTitle;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string JobTitle { get; set; }

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName} ({this.JobTitle})";
        }
    }
}
