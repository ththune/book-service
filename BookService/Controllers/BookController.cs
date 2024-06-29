using BookService.Data;
using BookService.DTOs;
using BookService.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<BookController> _logger;

        public BookController(IConfiguration configuration, ILogger<BookController> logger)
        {
            _dataContext = new DataContext(configuration);
            _logger = logger;
        }

        [HttpGet(Name = "GetBooks")]
        public ActionResult<IEnumerable<BookSimple>> GetBooks()
        {
            IEnumerable<BookSimple> books = _dataContext.Books.
                 Select(x => new BookSimple
                 {
                     BookId = x.BookId,
                     BookTitle = x.BookTitle,
                     BookIsbn = x.BookIsbn,
                     BookPublishedDate = x.BookPublishedDate,
                     BookCopiesAvailable = x.BookCopiesAvailable,
                 });

            return Ok(books);
        }

        [HttpGet("{bookId}")]
        public ActionResult<BookSimple> GetBookById(int bookId)
        {
            Book? dbBook = _dataContext.Books
               .Where(book => book.BookId == bookId)
               .FirstOrDefault();

            if (dbBook == null)
            {
                return NotFound();
            }

            BookSimple book = new BookSimple
            {
                BookId = dbBook.BookId,
                BookTitle = dbBook.BookTitle,
                BookIsbn = dbBook.BookIsbn,
                BookPublishedDate = dbBook.BookPublishedDate,
                BookCopiesAvailable = dbBook.BookCopiesAvailable
            };

            return Ok(book);

        }

        [HttpPost(Name = "AddBook")]
        public ActionResult AddBook()
        {
            return BadRequest("Not yet implemented");
        }
    }
}
