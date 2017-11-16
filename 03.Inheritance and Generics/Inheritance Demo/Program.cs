using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance_Demo
{
    public class Program
    {
        public static void Main()
        {
            var student = new Student("pmg","svetlin",17);
            student.Age = 5;
            student.School = "PMG";
            Console.WriteLine(student.Age);
        }
    }
}
