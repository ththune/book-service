using BookService.Data;
using BookService.DTOs.Book;
using BookService.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;
using System.Linq;
using BookService.DTOs.Author;
using BookService.DTOs.SignIn;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Microsoft.AspNetCore.Identity;

namespace BookService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IConfiguration configuration, ILogger<AuthController> logger)
        {
            _dataContext = new DataContext(configuration);
            _logger = logger;
        }

        [HttpPost("Login")]
        public ActionResult<String> SignIn(SignInRequest signInRequest)
        {

            BookService.Models.User? dbUser = _dataContext.Users
                .Where(user => user.UserUsername == signInRequest.Username)
                .FirstOrDefault();

            if (dbUser == null)
            {
                return Unauthorized("Invalid user credentials");
            }

            IPasswordHasher<object> hasher = new PasswordHasher<object>();
            PasswordVerificationResult result =
                hasher.VerifyHashedPassword(user: null, hashedPassword: dbUser.UserPasswordHash, providedPassword: signInRequest.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Invalid user credentials");
            }

            return Ok("Sign in successful");
        }
    }
}
