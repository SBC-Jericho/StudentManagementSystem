using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Services.SubjectService
{
    public class SubjectService : ISubjectService
    {
        // Dependency injection
        private readonly DataContext _context;

        // constructor to inject the DataContext again
        public SubjectService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Subject>> AddSubject(SubjectDTO subject)
        {
            var newSubject = new Subject
            {
                Name = subject.Name,
            };

            _context.Add(newSubject);
            await _context.SaveChangesAsync();
            return await _context.Subjects.ToListAsync();
        }

        public async Task<List<Subject>?> DeleteSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject is null)
                return null;

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();


            return await _context.Subjects.ToListAsync();   
        }

        public async Task<List<Subject>> GetAllSubject()
        {
            var subject = await _context.Subjects.ToListAsync();
            return subject;
        }

        public async Task<Subject?> GetSingleSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject is null)
                return null;
            return subject;
        }

        public async Task<List<Subject>?> UpdateSubject(int id, SubjectDTO request)
        {
            var subject = await _context.Subjects
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
            if (subject is null)
                return null;

            subject.Name = request.Name;
            
            await _context.SaveChangesAsync();

            return await _context.Subjects.ToListAsync();
        }
    }
}
