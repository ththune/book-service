namespace BookService.DTOs.Author
{
    public class AuthorCreateRequest
    {
        public string AuthorFirstName { get; set; } = string.Empty;
        public string AuthorLastName { get; set; } = string.Empty;
        public AuthorCreateRequest()
        {

        }
    }
}
