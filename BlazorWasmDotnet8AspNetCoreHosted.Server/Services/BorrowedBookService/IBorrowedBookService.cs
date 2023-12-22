using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Services.BorrowedBookService
{
    public interface IBorrowedBookService
    {
        Task<int> AddBorrowedBook(LibraryDTO request);

        Task<List<BorrowedBooks>> GetAllBorrowedBooks();

        Task<List<BorrowedBooks>> GetSingleBorrowedBooks(int id);
    }
}
