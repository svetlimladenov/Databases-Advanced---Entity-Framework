using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_1.Define_a_class_Person
{
    public class Engine
    {
        private bool IsRunning;

        public Engine()
        {
            IsRunning = true;
        }

        public void Run()
        {
            var n = int.Parse(Console.ReadLine());
            var persons = new List<Person>();
            for (int i = 0; i < n; i++)
            {
                var currentPersonInput = Console.ReadLine().Split();
                var name = currentPersonInput[0];
                var age = int.Parse(currentPersonInput[1]);
                persons.Add(new Person(name,age));
            }

            foreach (var person in persons.OrderBy(p => p.Name))
            {
                if (person.Age > 30)
                {
                    Console.WriteLine($"{person.Name} - {person.Age}");
                }
            }

            IsRunning = false;
        }
    }
}
