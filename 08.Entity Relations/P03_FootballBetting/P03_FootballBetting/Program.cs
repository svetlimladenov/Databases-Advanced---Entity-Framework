using System;
using P03_FootballBetting.Data;

namespace P03_FootballBetting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var db = new FootballBettingContext())
            {
                db.Database.EnsureCreated();
            }
        }
    }
}
