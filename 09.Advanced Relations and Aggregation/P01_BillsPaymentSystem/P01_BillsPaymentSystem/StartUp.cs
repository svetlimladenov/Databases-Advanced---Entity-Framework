using System;
using P01_BillsPaymentSystem.App;
using P01_BillsPaymentSystem.Data;

namespace P01_BillsPaymentSystem.App
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var db = new BillsPaymentSystemContext())
            {
                db.Database.EnsureCreated();
            } 
        }
    }
}
