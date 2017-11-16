using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P02_DatabaseFirst.Data.HomeworkExercises
{
    public class Find_Latest_10_Projects
    {
        public Find_Latest_10_Projects()
        {
            
        }

        public void Run()
        {
            using (var db = new SoftUniContext())
            {
                var projects = db.Projects
                    .Select(p => new
                    {
                        ProjectName = p.Name,
                        Desctiption = p.Description,
                        StartDate = p.StartDate
                    })
                    .ToList()
                    .OrderByDescending(p => p.StartDate)
                    .Take(10)
                    .OrderBy(p => p.ProjectName);

                foreach (var project in projects)
                {
                    Console.WriteLine(project.ProjectName);
                    Console.WriteLine(project.Desctiption);
                    Console.WriteLine(project.StartDate);
                }

            }
        }
    }
}
