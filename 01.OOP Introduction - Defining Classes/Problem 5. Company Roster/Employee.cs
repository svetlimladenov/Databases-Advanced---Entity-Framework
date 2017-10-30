using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_5.Company_Roster
{
    public class Employee
    {
        private string name;
        private double salary;
        private string position;
        private string department;
        private string email;
        private int age;

        public Employee(string name, double salary, string position, string department, string email = "n/a", int age = -1)
        {
            this.name = name;
            this.salary = salary;
            this.position = position;
            this.department = department;
            this.email = email;
            this.age = age;
        }
        
        public int Age
        {
            get { return this.age; }
            set { this.age = value; }
        }

        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }

        public string Department
        {
            get { return this.department; }
            set { this.department = value; }
        }

        public string Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public double Salary
        {
            get { return this.salary; }
            set { this.salary = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

    }
}
