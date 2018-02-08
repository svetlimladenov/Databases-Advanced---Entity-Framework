using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using BookShop.Models;

namespace BookShop
{
    using BookShop.Data;
    using BookShop.Initializer;
    
    class StartUp
    {
        static void Main()
        {
            using (var db = new BookShopContext())
            {
                Console.WriteLine(GetMostRecentBooks(db));
                //Console.WriteLine(GetTotalProfitByCategory(db)); 12.
                
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
                .Where(b => b.AgeRestriction == (AgeRestriction)enumValue)
                .Select(b => b.Title)
                .OrderBy(t => t)
                .ToArray();



            var result = string.Join(Environment.NewLine, titles);

            return result;
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context
                .Books
                .Where(b => b.Copies < 5000 && b.EditionType == EditionType.Gold)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToArray();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context
                .Books
                .Where(b => b.Price > 40.00m)
                .OrderByDescending(b => b.Price)
                .Select(b => new
                {
                    b.Title,
                    b.Price
                })
                .ToArray();

            var result = new StringBuilder();
            foreach (var book in books)
            {
                result.AppendLine($"{book.Title} - ${book.Price:F2}");
            }

            return result.ToString();
        }

        public static string GetBooksNotRealeasedIn(BookShopContext context, int year)
        {
            var books = context
                .Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToArray();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var categories = input.Split(new[] { " ", "\t", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var titles = context
                .Books
                .Where(b => b.BookCategories.Any(c => categories.Contains(c.Category.Name)))
                .Select(b => b.Title)
                .OrderBy(t => t)
                .ToArray();

            var result = string.Join(Environment.NewLine, titles);
            return result;
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var dateParsed = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.CurrentCulture);
            var books = context
                .Books
                .Where(b => b.ReleaseDate < dateParsed)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => new
                {
                    Title = b.Title,
                    Edition = b.EditionType,
                    Price = b.Price,
                })
                .ToArray();

            var result = new StringBuilder();
            foreach (var book in books)
            {
                result.AppendLine($"{book.Title} - {book.Edition} - ${book.Price:F2}");
            }

            return result.ToString().Trim();
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var booksWithAuthorNameEdningWith =
                context.Books
                    .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                    .Select(b => new
                    {
                        AuthorName = b.Author.FirstName + " " + b.Author.LastName,
                        Title = b.Title,
                        b.BookId
                    })
                    .OrderBy(b => b.BookId)
                    .ToArray();

            var result = new StringBuilder();
            foreach (var bookAuthor in booksWithAuthorNameEdningWith)
            {
                result.AppendLine($"{bookAuthor.Title} ({bookAuthor.AuthorName})");
            }
            return result.ToString().Trim();
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var titles = context
                .Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToArray();

            return string.Join(Environment.NewLine, titles);
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var books = context
                .Books
                .Count(b => b.Title.Length > lengthCheck);

            return books;
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {

            var authors = context
                .Authors
                .Select(a => new
                {
                    Name = a.FirstName + " " + a.LastName,
                    Copies = a.Books.Select(b => b.Copies).Sum()
                })
                .OrderByDescending(a => a.Copies)
                .ToArray()
                .Select(a => $"{a.Name} - {a.Copies}");

            return string.Join(Environment.NewLine, authors);

            //var authorBooks = context
            //    .Authors
            //    .Select(a => new
            //    {
            //        books = a.Books,
            //        authorName = a.FirstName + " " + a.LastName
            //    })
            //    .ToArray();
            //var result = new Dictionary<string, int>();

            //foreach (var authorBook in authorBooks)
            //{

            //    var currentAuhtorCopies = 0;
            //    foreach (var book in authorBook.books)
            //    {
            //        currentAuhtorCopies += book.Copies;
            //    }
            //    result.Add(authorBook.authorName, currentAuhtorCopies);
            //}

            //var sb = new StringBuilder();
            //foreach (var kvp in result.OrderByDescending(k => k.Value))
            //{
            //    sb.AppendLine($"{kvp.Key} - {kvp.Value}");
            //}

            //return sb.ToString().Trim();
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
            => string.Join(Environment.NewLine, context.Categories
                .Select(c => new
                {
                    Name = c.Name,
                    TotalProffit = c.CategoryBooks
                        .Select(cb => cb.Book.Price * cb.Book.Copies)
                        .Sum()
                })
                .OrderByDescending(c => c.TotalProffit)
                .ThenBy(c => c.Name)
                .Select(c => $"{c.Name} ${c.TotalProffit:F2}"));

        public static string GetMostRecentBooks(BookShopContext context)
        {
            var categories = context
                .Categories
                .Select(c => new
                {
                    CategoryName = c.Name,
                    Last3Books = c.CategoryBooks.Select(a => a.Book).OrderByDescending(a => a.ReleaseDate),
                    BookCount = c.CategoryBooks
                        .Select(cb => cb.Book)
                        .Count(),
                })
                //.OrderBy(c => c.BookCount) wrong by judge
                .OrderBy(c => c.CategoryName)
                .ToArray();
               
            var result = new StringBuilder();
            foreach (var category in categories)
            {
                result.AppendLine("--" + category.CategoryName);
                foreach (var book in category.Last3Books.Take(3))
                {
                    result.AppendLine($"{book.Title} ({book.ReleaseDate.Value.Year})");
                }
            }

            return result.ToString().Trim();
        }
    }
}
