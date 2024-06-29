namespace BookService.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public string BookIsbn { get; set; } = string.Empty;
        public DateOnly BookPublishedDate { get; set; } = DateOnly.MaxValue;
        public byte BookCopiesAvailable { get; set; } = 0;

        // Database metadata.
        public DateTime BookCreatedStamp { get; set; } = DateTime.Now;

        // Database metadata.
        public DateTime? BookUpdatedStamp { get; set; }

        // Navigation property
        public ICollection<BookAuthor> BookAuthors { get; set; }
        public Book()
        {

        }

    }
}
