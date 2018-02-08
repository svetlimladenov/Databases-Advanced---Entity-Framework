using System;
using System.Collections.Generic;
using System.Linq;
using BookShop.Data;
using BookShop.Initializer;
using BookShop.Models;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

namespace BookShop.StartUp
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var input = Console.ReadLine();
            using (var context = new BookShopContext())
            {
                string result = GetBooksByAgeRestriction(context, input);
                Console.WriteLine(result);
            }

        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            int enumValue = -1;
            switch (command.ToLower())
            {
                case "minor":
                    enumValue = 0;
                    break;
                case "teen":
                    enumValue = 1;
                    break;
                case "adult":
                    enumValue = 2;
                    break;
            }


            var titles = context
                .Books
                .Where(b => b.AgeRestriction == (AgeRestriction) enumValue)
                .Select(b => b.Title)
                .OrderBy(t => t)
                .ToArray();
                
                
                
            var result = string.Join(Environment.NewLine, titles);

            return result;
        }


    }
}
