namespace BookService.DTOs.Book
{
    public class BookUpdateResponse
    {
        public int BookId { get; set; }
        public string? BookTitle { get; set; }
        public string? BookIsbn { get; set; }
        public DateOnly? BookPublishedDate { get; set; }
        public BookUpdateResponse()
        {

        }
    }
}
