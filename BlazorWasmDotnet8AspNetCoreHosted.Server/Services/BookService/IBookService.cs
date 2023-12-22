using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Services.BookService
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBook();
        Task<Book?> GetSingleBook(int id);
        Task<List<Book>> AddBook(BookDTO Book);
        Task<List<Book>?> UpdateBook(int id, BookDTO request);
        Task<List<Book>?> DeleteBook(int id);
    }
}
