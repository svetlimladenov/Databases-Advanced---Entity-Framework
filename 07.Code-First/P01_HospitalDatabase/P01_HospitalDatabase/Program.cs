using System;
using P01_HospitalDatabase.Data;

namespace P01_HospitalDatabase
{
    class Program
    {
        public static void Main(string[] args)
        {
            using (var db = new HospitalContext())
            {
                db.Database.EnsureCreated();
            }
        }
    }
}
