using BookStoreAPI.Models;
using BookStoreAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? search,
            [FromQuery] int? categoryId,
            [FromQuery] int? authorId,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var books = await _bookService.GetAllBooks(search, categoryId, authorId, minPrice, maxPrice, page, pageSize);
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookService.GetBookById(id);

            if (book == null)
            {
                return NotFound(new { message = "Book not found." });
            }

            return Ok(book);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateBookDto dto)
        {
            if (string.IsNullOrEmpty(dto.Title))
            {
                return BadRequest(new { message = "Book title is required." });
            }

            try
            {
                var book = await _bookService.CreateBook(dto);
                return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBookDto dto)
        {
            try
            {
                var book = await _bookService.UpdateBook(id, dto);

                if (book == null)
                {
                    return NotFound(new { message = "Book not found." });
                }

                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _bookService.DeleteBook(id);

            if (!deleted)
            {
                return NotFound(new { message = "Book not found." });
            }

            return Ok(new { message = "Book deleted successfully." });
        }
    }
}
