using System;
using System.Linq;
using Forum.Data;
using Forum.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Forum
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var context = new ForumDbContext();

            //var tags = new[]
            //{
            //    new Tag {Name = "C#"},
            //    new Tag {Name = "Programming"},
            //    new Tag{Name = "Praise"},
            //    new Tag {Name = "Microsoft"}

            //};

            //var postTags = new[]
            //{
            //    new PostTag(){PostId = 1,Tag = tags[0]},
            //    new PostTag(){PostId = 1,Tag = tags[1]},
            //    new PostTag(){PostId = 1,Tag = tags[2]},
            //    new PostTag(){PostId = 1,Tag = tags[3]},
            //};

            //context.PostTags.AddRange(postTags);

            //context.SaveChanges();
            //ResetDatabase(context);

            //var categories = context.Categories
            //    .Include(c => c.Posts)
            //    .ThenInclude(p => p.Author)
            //    .Include(c => c.Posts)
            //    .ThenInclude(p => p.Replies)
            //    .ThenInclude(r => r.Author)
            //    .ToArray();

            var categories = context.Categories
                .Select(c => new
                {
                    Name = c.Name,
                    Posts = c.Posts.Select(p => new
                    {
                        AuthorUsername = p.Author.Username,
                        Title = p.Title,
                        Content = p.Content,
                        Tags = p.PostTags.Select(t => t.Tag.Name),
                        Replies = p.Replies.Select(r => new
                        {
                            RepliesContent = r.Content,
                            ReplieAuthor = r.Author.Username
                        })
                    })
                })
                .ToArray();

            foreach (var category in categories)
            {
                Console.WriteLine($"{category.Name} ({category.Posts.Count()})");

                foreach (var post in category.Posts)
                {
                    Console.WriteLine($"--{post.Title}: {post.Content}");
                    Console.WriteLine($"--by {post.AuthorUsername}");

                    Console.WriteLine("tags : "+string.Join(", ",post.Tags));  
                    foreach (var replies in post.Replies)
                    {
                        Console.WriteLine($"-----{replies.RepliesContent} from {replies.ReplieAuthor}");
                    }
                                 
                }
            }
        }

        private static void ResetDatabase(ForumDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Database.Migrate();

            Seed(context);
        }

        private static void Seed(ForumDbContext context)
        {
            var users = new[]
            {
                    new User("gosho", "123"),
                    new User("svetli", "3234"),
                    new User("pesho", "123"),
                    new User("ivan", "123")
                };

            context.Users.AddRange(users);

            var categories = new[]
            {
                    new Category("C#"),
                    new Category("Support"),
                    new Category("Python"),
                    new Category("EF KOR"),
                };

            context.Categories.AddRange(categories);

            var posts = new[]
            {
                    new Post("C# Rulz", "Trueeeasdf", categories[0], users[0]),
                    new Post("Python Rulz", "Trueeeasdf", categories[2], users[2]),
                    new Post("My PC doesnt turn on", "Sad", categories[1], users[3]),

                };


            context.Posts.AddRange(posts);

            var replies = new[]
            {
                    new Reply("Turn it on", posts[2], users[1]),
                    new Reply("Yep", posts[0], users[0]),
                   // new Reply("Sure",posts[1],users[3]), 
                };

            context.Replies.AddRange(replies);


            context.SaveChanges();

        }
    }
}
