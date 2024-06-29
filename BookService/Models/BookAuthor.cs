// This is a cross reference model between Book and Author.

namespace BookService.Models
{
    public class BookAuthor
    {
        public int BookAuthorId { get; set; }
        public int BookAuthorBookId { get; set; }
        public int BookAuthorAuthorId { get; set; }

        // Database metadata.
        public DateTime BookAuthorCreatedStamp { get; set; } = DateTime.Now;

        // Database metadata.
        public DateTime? BookAuthorUpdatedStamp { get; set; }

        public Book Book { get; set; }

        public Author Author { get; set; }

        public BookAuthor()
        {

        }

    }
}
