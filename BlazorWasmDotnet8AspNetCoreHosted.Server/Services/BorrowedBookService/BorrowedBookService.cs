using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Services.BorrowedBookService
{
    public class BorrowedBookService : IBorrowedBookService
    {
        private readonly DataContext _context;

        // constructor to inject the DataContext again
        public BorrowedBookService(DataContext context)
        {
            _context = context;
        }
        public async Task<int> AddBorrowedBook(LibraryDTO request)
        {
            var library = new Library
            {  
                DateBorrowed = DateTime.Now,
                BorrowedBooks = new List<BorrowedBooks>(),
                DateReturn = DateTime.Now,
                BorrowedReason = request.BorrowedReason,
                StudentId = request.StudentId,  
             };

            foreach (var borrowedBook in request.BorrowedBooks) 
            {
                int alreadyBorrowed = await _context.BorrowedBooks
              .Where(b => b.Library.StudentId == request.StudentId && b.BookId == borrowedBook.BookId)
              .Select(b => b.Id)
              .FirstOrDefaultAsync();

                if (alreadyBorrowed == 0)
                {
                    var borrowed = new BorrowedBooks
                    {
                        BookId = borrowedBook.BookId,
                    };

                    library.BorrowedBooks.Add(borrowed);
                    _context.BorrowedBooks.Add(borrowed);
                }
                else
                {
                    return 0;
                }
            }

            _context.Libraries.Add(library);
            await _context.SaveChangesAsync();
            return library.Id;
        }

        public async Task<List<BorrowedBooks>> GetAllBorrowedBooks()
        {
            var borrowed = await _context.BorrowedBooks
              .Include(u => u.Book)
              .Include(u => u.Library)
              .ToListAsync();

            return borrowed;
        }

        public async Task<List<BorrowedBooks>> GetSingleBorrowedBooks(int id)
        {
            var book = await _context.BorrowedBooks
                .Include(u => u.Library)
                    .ThenInclude(u => u.Student)
                .Include(u => u.Book)
                .Where(u => u.Library.StudentId == id)
                .ToListAsync();

             return book;
        }
    }
}
