using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Services.ProfessorService
{
    public class ProfessorService : IProfessorService
    {
        // Dependency injection
        private readonly DataContext _context;

        // constructor to inject the DataContext again
        public ProfessorService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Professor>> AddProfessor(ProfessorDTO prof)
        {
            var newProfessor = new Professor
            {
                FirstName = prof.FirstName,
                LastName = prof.LastName,
                BirthDate = prof.BirthDate,
                Contact = prof.Contact,
                Address = prof.Address,
                Image = prof.Image,
            };

            _context.Add(newProfessor);
            await _context.SaveChangesAsync();
            return await _context.Professors.ToListAsync();
        }

        public async Task<List<Professor>?> DeleteProfessor(int id)
        {
            var professor = await _context.Professors.FindAsync(id);
            if (professor is null)
                return null;

            _context.Professors.Remove(professor);
            await _context.SaveChangesAsync();


            return await _context.Professors.ToListAsync();
        }

        public async Task<List<Professor>> GetAllProfessor()
        {
            var professor = await _context.Professors
                .Include(u => u.User)
                .ToListAsync();
            return professor;
        }

        public async Task<Professor?> GetSingleProfessor(int id)
        {
            var professor = await _context.Professors.FindAsync(id);
            if (professor is null)
                return null;
            return professor;
        }

        public async Task<List<Professor>?> UpdateProfessor(int id, ProfessorDTO request)
        {
            var professor = await _context.Professors
                .Include(u => u.User)
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
            if (professor is null)
                return null;

            professor.FirstName = request.FirstName;
            professor.LastName = request.LastName;
            professor.BirthDate = request.BirthDate;
            professor.Contact = request.Contact;
            professor.Address = request.Address;
            professor.Image = request.Image;

            await _context.SaveChangesAsync();

            return await _context.Professors.ToListAsync();
        }

    }
}
