using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance_Demo
{
    public class Student : Person
    {
        private string school;

        public Student(string school,string name,int age) 
            : base(name,age)
        {
            this.School = school;
            this.Age = age;
            this.Name = name;
        }
        

        public string School
        {
            get { return this.school; }
            set { this.school = value; }
        }

    }
}
