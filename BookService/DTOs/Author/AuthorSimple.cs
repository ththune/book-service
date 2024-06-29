namespace BookService.DTOs.Author
{
    public class AuthorSimple
    {
        public int AuthorId { get; set; }
        public string AuthorFirstName { get; set; } = string.Empty;
        public string AuthorLastName { get; set; } = string.Empty;
        public AuthorSimple()
        {

        }
    }
}
