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
        public ActionResult AddBook(BookCreateRequest book)
        {
            Book newBook = new Book
            {
                BookTitle = book.BookTitle,
                BookIsbn = book.BookIsbn,
                BookPublishedDate = book.BookPublishedDate,
                BookCopiesAvailable = book.BookCopiesAvailable,
            };

            // Forward the new book to the database and save it.
            _dataContext.Books.Add(newBook);
            int saveChanges = _dataContext.SaveChanges();

            if (saveChanges <= 0)
            {
                return BadRequest("Failed to add the book.");
            }

            return CreatedAtAction(nameof(GetBookById), new Book { }, newBook);
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
            if (book.BookCopiesAvailable != null) dbBook.BookCopiesAvailable = (byte)book.BookCopiesAvailable;
            dbBook.BookUpdatedStamp = DateTime.Now;

            // Save the changes on dbBook to the database 
            int saveChanges = _dataContext.SaveChanges();

            if (saveChanges <= 0)
            {
                return BadRequest("Failed to update the book.");
            }

            return Ok();
        }
    }
}
