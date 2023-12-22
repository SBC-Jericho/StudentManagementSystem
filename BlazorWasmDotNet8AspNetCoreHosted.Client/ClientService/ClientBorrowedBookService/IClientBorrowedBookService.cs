using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;

namespace BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientBorrowedBookService
{
    public interface IClientBorrowedBookService
    {
        List<BorrowedBooks> ClientBorrowedBooks { get; set; }

        Task <int>AddBorrowedBooks(LibraryDTO request);

        Task<List<BorrowedBooks>> GetAllBorrowedBook();

        Task<List<BorrowedBooks>> GetSingleBorrowedBooks(int id);
    }
}
