using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;

namespace BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientBookService
{
    public interface IClientBookService
    {
        List<Book> ClientBook { get; set; }
        Task<List<Book>> GetAllBook();
        Task<Book?> GetSingleBook(int id);
        Task AddBook(Book Book);
        Task UpdateBook(int id, Book request);
        Task DeleteBook(int id);
    }
}
