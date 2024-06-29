namespace BookService.DTOs
{
    public class BookSimple
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public string BookIsbn { get; set; } = string.Empty;
        public DateOnly BookPublishedDate { get; set; } = DateOnly.MaxValue;
        public byte BookCopiesAvailable { get; set; } = 0;
        public BookSimple()
        {

        }
    }
}
