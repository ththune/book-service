using BookService.DTOs.Author;

namespace BookService.DTOs.Book
{
    public class BookFull
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public string BookIsbn { get; set; } = string.Empty;
        public DateOnly BookPublishedDate { get; set; } = DateOnly.MaxValue;
        public IEnumerable<AuthorSimple> Authors { get; set; } = [];
        public BookFull()
        {

        }
    }
}
