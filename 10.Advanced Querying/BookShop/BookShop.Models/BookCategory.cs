namespace BookShop.Models
{
    public class BookCategory
    {
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int CategotyId { get; set; }
        public Category Category { get; set; }
    }
}
