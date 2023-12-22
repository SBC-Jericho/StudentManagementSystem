using BlazorWasmDotnet8AspNetCoreHosted.Server.Services.BorrowedBookService;
using BlazorWasmDotnet8AspNetCoreHosted.Server.Services.EnrolledSubjectsService;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowedBooksController : ControllerBase
    {
        private readonly IBorrowedBookService _borrowedBookService;
        public BorrowedBooksController(IBorrowedBookService borrowedBookService)
        {
            _borrowedBookService = borrowedBookService;
        }
        [HttpGet]
        public async Task<ActionResult<List<BorrowedBooks>>> GetAllBorrowedBooks()
        {

            return await _borrowedBookService.GetAllBorrowedBooks();
        }

        [HttpGet("book-details/{id}")]
        public async Task<List<BorrowedBooks>> GetSingleBorrowedBooks(int id)
        {
            var result = await _borrowedBookService.GetSingleBorrowedBooks(id);
            return result;

        }
        [HttpPost("add-borrowed-book")]
        public async Task<ActionResult<int>> AddBorrowedBook(LibraryDTO request)
        {
            var result = await _borrowedBookService.AddBorrowedBook(request);
            if (result == 0) 
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
