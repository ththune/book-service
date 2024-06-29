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
        public IEnumerable<BookSimple> Get()
        {
            IEnumerable<BookSimple> bookSimples = _dataContext.Books.
                 Select(x => new BookSimple
                 {
                     BookId = x.BookId,
                     BookTitle = x.BookTitle,
                     BookIsbn = x.BookIsbn,
                     BookPublishedDate = x.BookPublishedDate,
                     BookCopiesAvailable = x.BookCopiesAvailable,
                 });

            return bookSimples;
        }
    }
}
