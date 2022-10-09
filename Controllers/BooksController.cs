using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PostgreSQL.Demo.API.Entities;
using PostgreSQL.Demo.API.Models.Books;
using PostgreSQL.Demo.API.Services;

namespace PostgreSQL.Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private IMapper _mapper;

        public BooksController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        // GET: api/<BooksController>
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            IEnumerable<Book> books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            Book book = await _bookService.GetBookByIdAsync(id);
            return Ok(book);
        }

        // POST api/<BooksController>
        [HttpPost]
        public async Task<IActionResult> CreateBook(CreateBookRequest model)
        {
            int book = await _bookService.CreateBook(model);

            if (book != 0)
            {
                return Ok("The book was successfully added to the database");
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "The book was successfully added to the database");
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, UpdateBookRequest model)
        {
            await _bookService.UpdateBook(id, model);
            return Ok("The book was successfully updated in the database");
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBook(id);
            return Ok("The book was successfully deleted in the database");
        }
    }
}
