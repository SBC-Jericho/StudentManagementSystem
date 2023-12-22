using BlazorWasmDotnet8AspNetCoreHosted.Server.Services.BookService;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetAllBook()
        {

            return await _bookService.GetAllBook();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetSingleBook(int id)
        {
            var result = await _bookService.GetSingleBook(id);
            if (result is null)
                return NotFound("Book Not Found");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Book>>> AddBook(BookDTO Book)
        {
            var result = await _bookService.AddBook(Book);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> UpdateBook(int id, BookDTO request)
        {
            var result = await _bookService.UpdateBook(id, request);
            if (result is null)
                return NotFound("Book Not Found");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBook(id);
            if (result is null)
                return NotFound("Book Not Found");

            return Ok(result);
        }
    }
}
