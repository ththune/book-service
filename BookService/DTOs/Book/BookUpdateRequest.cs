namespace BookService.DTOs.Book
{
    public class BookUpdateRequest
    {
        public string? BookTitle { get; set; }
        public string? BookIsbn { get; set; }
        public DateOnly? BookPublishedDate { get; set; }
        public BookUpdateRequest()
        {

        }
    }
}
