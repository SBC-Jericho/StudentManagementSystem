using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;

namespace BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientBookService
{
    public interface IClientBookService
    {
        List<Book> ClientBook { get; set; }
        Task<List<Book>> GetAllBook();
        Task<Book?> GetSingleBook(int id);
        Task AddBook(BookDTO Book);
        Task UpdateBook(int id, Book request);
        Task DeleteBook(int id);
    }
}
