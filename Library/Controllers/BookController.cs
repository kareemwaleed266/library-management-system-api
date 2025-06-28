using Library.Data.Entites.IdentityEntities;
using Library.Service.BookService;
using Library.Service.BookService.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;

        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddBookAsync")]

        public async Task<ActionResult<BookResultDto>> AddBookAsync(BookDto input)
        {
            var book = await _bookService.AddBookAsync(input);

            if (book == null)
                return BadRequest("Error on Adding Book");

            return Ok(book);
        }

        //[HttpDelete("{bookId}")]

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteBook/{bookId}")]

        public async Task<ActionResult> DeleteBook(int? bookId)
        {
            await _bookService.DeleteBook(bookId);
            return NoContent();
        }


        [Authorize]
        [HttpGet("GetAllBooksAsync")]
        public async Task<ActionResult<IReadOnlyList<BookResultDto>>> GetAllBooksAsync()
        {
            var books = await _bookService.GetAllBooksAsync();
            if (books == null)
                return BadRequest("Books Not Found");

            return Ok(books);
        }

        [Authorize]
        [HttpGet("GetBook/{bookId}")]
        public async Task<ActionResult<BookResultDto>> GetBookByIdAsync(int? bookId)
        {
            var book = await _bookService.GetBookByIdAsync(bookId);

            if (book == null)
                return BadRequest("Book Not Found");

            return Ok(book);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{bookId}")]
        public async Task<ActionResult<BookResultDto>> UpdateBookAsync(int? bookId, [FromBody] BookDto input)

        {
            var book = await _bookService.UpdateBookAsync(bookId, input);

            if (book == null)
                return BadRequest("Error on Updating Book Details");

            return Ok(book);
        }
    }
}
