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
    public class AuthorController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<AuthorController> _logger;

        public AuthorController(IConfiguration configuration, ILogger<AuthorController> logger)
        {
            _dataContext = new DataContext(configuration);
            _logger = logger;
        }

        [HttpPost("Book/{bookId}")]
        public ActionResult<String> AddAuthorsToBook(AuthorCreateRequest[] authors, int bookId)
        {

            Book? dbBook = _dataContext.Books
               .Where(book => book.BookId == bookId)
               .FirstOrDefault();

            if (dbBook == null)
            {
                return NotFound($"Could not find book with id {bookId}");
            }

            List<Author> newAuthors = new List<Author>();

            foreach (AuthorCreateRequest authorCreateRequest in authors)
            {
                Author newAuthor = new Author
                {
                    AuthorFirstName = authorCreateRequest.AuthorFirstName,
                    AuthorLastName = authorCreateRequest.AuthorLastName,
                };

                newAuthors.Add(newAuthor);
                _dataContext.Authors.Add(newAuthor);
            }

            if (_dataContext.SaveChanges() <= 0)
            {
                return BadRequest("Failed to add the authors.");
            }

            // Connect the new AuthorIds with the BookId 
            // through the BookAuthor table.
            foreach (Author author in newAuthors)
            {
                BookAuthor newBookAuthor = new BookAuthor
                {
                    BookAuthorBookId = bookId,
                    BookAuthorAuthorId = author.AuthorId,
                };

                _dataContext.BookAuthors.Add(newBookAuthor);
            }

            if (_dataContext.SaveChanges() <= 0)
            {
                return BadRequest($"Failed to link the authors with book id {bookId}");
            }



            return Created();
        }
    }
}
