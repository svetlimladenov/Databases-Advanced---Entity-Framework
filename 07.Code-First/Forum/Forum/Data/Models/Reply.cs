using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Data.Models
{
    public class Reply
    {
        public Reply()
        {
            
        }

        public Reply(string content, Post post, User author)
        {
            Content = content;
            Post = post;
            this.Author = author;
        }

        public int Id { get; set; }

        public string Content { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }

        //[ForeignKey("Author")]
        public int AuthorId { get; set; }

        public User Author { get; set; }
    }
}
