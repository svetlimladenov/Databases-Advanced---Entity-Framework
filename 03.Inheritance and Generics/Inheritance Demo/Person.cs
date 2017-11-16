using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance_Demo
{
    public class Person
    {
        private string name;
        private int age;

        public Person(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public int Age      
        {
            get { return this.age; }
            set { this.age = value; }
        }


        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

    }
}
