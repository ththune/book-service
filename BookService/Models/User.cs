namespace BookService.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserUsername { get; set; } = string.Empty;
        public string UserPasswordHash { get; set; } = string.Empty;

        public User()
        {

        }

    }
}
