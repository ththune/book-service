namespace BookService.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string AuthorFirstName { get; set; } = string.Empty;
        public string AuthorLastName { get; set; } = string.Empty;

        // Database metadata.
        public DateTime AuthorCreatedStamp { get; set; } = DateTime.Now;

        // Database metadata.
        public DateTime? AuthorUpdatedStamp { get; set; }

        // Navigation property
        public ICollection<BookAuthor> BookAuthors { get; set; }
        public Author()
        {

        }

    }
}
