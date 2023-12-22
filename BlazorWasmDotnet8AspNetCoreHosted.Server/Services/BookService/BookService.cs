using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Services.BookService
{
    public class BookService : IBookService
    {
        // Dependency injection
        private readonly DataContext _context;

        // constructor to inject the DataContext again
        public BookService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Book>> AddBook(BookDTO book)
        {
            var newBook = new Book
            {
                Title = book.Title,
                Description = book.Description,
            };

            _context.Add(newBook);
            await _context.SaveChangesAsync();
            return await _context.Books.ToListAsync();
        }

        public async Task<List<Book>?> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book is null)
                return null;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();


            return await _context.Books.ToListAsync();
        }

        public async Task<List<Book>> GetAllBook()
        {
            var book = await _context.Books.ToListAsync();
            return book;
        }

        public async Task<Book?> GetSingleBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book is null)
                return null;
            return book;
        }

        public async Task<List<Book>?> UpdateBook(int id, BookDTO request)
        {
            var book = await _context.Books
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
            if (book is null)
                return null;

            book.Title = request.Title;
            book.Description = request.Description; 

            await _context.SaveChangesAsync();

            return await _context.Books.ToListAsync();
        }
    }
}
