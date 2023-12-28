using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
using Microsoft.EntityFrameworkCore;
using static MudBlazor.CategoryTypes;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Services.EnrolledSubjectsService
{
    public class EnrolledSubjectsService : IEnrolledSubjectsService
    {
        // Dependency injection
        private readonly DataContext _context;

        // constructor to inject the DataContext again
        public EnrolledSubjectsService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<EnrolledSubjects>> GetAllEnrolledSubject()
        {
            var enrolled = await _context.EnrolledSubjects
                .Include(u => u.Subject)
                .Include(u => u.Enrollment)
                .ToListAsync();

            return enrolled;
        }
        public async Task<int> AddEnrolledSubject(EnrollmentDTO request)
        {
            var enrollment = new Enrollment
            {
                DateCreated = DateTime.Now,
                EnrolledSubjects = new List<EnrolledSubjects>(),
                Semester = request.Semester,
                SchoolYear = request.SchoolYear,
                StudentId = request.StudentId
            };

            foreach (var enrolledSub in request.EnrolledSubjects)
            {
               int alreadyEnrolled = await _context.EnrolledSubjects
              .Where(b => b.Enrollment.StudentId == request.StudentId && b.SubjectId == enrolledSub.SubjectId)
              .Select(b => b.Id)
              .FirstOrDefaultAsync();

                if (alreadyEnrolled == 0)
                {
                    var enrolled = new EnrolledSubjects
                    {
                        SubjectId = enrolledSub.SubjectId
                    };
                    enrollment.EnrolledSubjects.Add(enrolled);
                    _context.EnrolledSubjects.Add(enrolled);
                }
                else 
                {
                    return 0;
                }
            }

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
            return enrollment.Id;
        }

        public async Task<List<EnrolledSubjects>> GetSingleEnrolledSubjects(int id)
        {
            List <EnrolledSubjects> subjects = await _context.EnrolledSubjects
               .Include(u => u.Enrollment)
                    .ThenInclude(u => u.Student)
               .Include(u => u.Subject)
               .Where(u => u.Enrollment.StudentId == id)
               .ToListAsync();

            return subjects;
        
        }
    }

}
