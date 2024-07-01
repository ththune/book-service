using BookService.Data;
using BookService.DTOs.Book;
using BookService.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;
using System.Linq;
using BookService.DTOs.Author;

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
            IEnumerable<BookSimple> dbBooks = _dataContext.Books.
                 Select(x => new BookSimple
                 {
                     BookId = x.BookId,
                     BookTitle = x.BookTitle,
                     BookIsbn = x.BookIsbn,
                     BookPublishedDate = x.BookPublishedDate,
                 });

            return Ok(dbBooks);
        }

        [HttpGet("{bookId}")]
        public ActionResult<BookFull> GetBookById(int bookId)
        {
            Book? dbBook = _dataContext.Books
               .Where(book => book.BookId == bookId)
               .FirstOrDefault();

            if (dbBook == null)
            {
                return NotFound();
            }

            IEnumerable<AuthorSimple> dbAuthors = _dataContext.BookAuthors
                .Where(x => x.BookAuthorBookId == bookId)
                .Select(x => x.Author)
                .Select(x => new AuthorSimple
                {
                    AuthorId = x.AuthorId,
                    AuthorFirstName = x.AuthorFirstName,
                    AuthorLastName = x.AuthorLastName,
                })
                .ToList();

            BookFull book = new BookFull
            {
                BookId = dbBook.BookId,
                BookTitle = dbBook.BookTitle,
                BookIsbn = dbBook.BookIsbn,
                BookPublishedDate = dbBook.BookPublishedDate,
                Authors = dbAuthors,
            };

            return Ok(book);

        }

        [HttpPost(Name = "AddBook")]
        public ActionResult<BookCreateResponse> AddBook(BookCreateRequest book)
        {
            Book newBook = new Book
            {
                BookTitle = book.BookTitle,
                BookIsbn = book.BookIsbn,
                BookPublishedDate = book.BookPublishedDate,
            };

            // Forward the new book to the database and save it.
            _dataContext.Books.Add(newBook);
            int saveChanges = _dataContext.SaveChanges();

            if (saveChanges <= 0)
            {
                return BadRequest("Failed to add the book.");
            }

            return Ok(new BookCreateResponse
            {
                BookId = newBook.BookId,
                BookTitle = newBook.BookTitle,
                BookIsbn = newBook.BookIsbn,
                BookPublishedDate = newBook.BookPublishedDate
            });
        }

        [HttpPut("{bookId}")]
        public ActionResult EditBookById(int bookId, BookUpdateRequest book)
        {
            // Get the book to edit from the database.
            Book? dbBook = _dataContext.Books
               .Where(book => book.BookId == bookId)
               .FirstOrDefault();

            if (dbBook == null)
            {
                return NotFound();
            }

            // Only edit values that has been changed.
            if (book.BookTitle != null) dbBook.BookTitle = book.BookTitle;
            if (book.BookIsbn != null) dbBook.BookIsbn = book.BookIsbn;
            if (book.BookPublishedDate != null) dbBook.BookPublishedDate = (DateOnly)book.BookPublishedDate;
            dbBook.BookUpdatedStamp = DateTime.Now;

            // Save the changes on dbBook to the database 
            int saveChanges = _dataContext.SaveChanges();

            if (saveChanges <= 0)
            {
                return BadRequest("Failed to update the book.");
            }

            return Ok();
        }

        [HttpDelete("{bookId}")]
        public ActionResult DeleteBook(int bookId)
        {
            // Get the book to delete from the database, if it exist.
            Book? dbBook = _dataContext.Books
               .Where(book => book.BookId == bookId)
               .FirstOrDefault();

            if (dbBook == null)
            {
                return NotFound();
            }

            _dataContext.Books.Remove(dbBook);
            int saveChanges = _dataContext.SaveChanges();
            if (saveChanges <= 0)
            {
                return BadRequest("Failed to delete the book.");
            }

            return Ok();
        }
    }
}
