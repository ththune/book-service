namespace BookService.DTOs.Book
{
    public class BookCreateRequest
    {
        public string BookTitle { get; set; } = string.Empty;
        public string BookIsbn { get; set; } = string.Empty;
        public DateOnly BookPublishedDate { get; set; } = DateOnly.MaxValue;
        public BookCreateRequest()
        {

        }
    }
}
