using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static MudBlazor.CategoryTypes;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Services.EnrolledSubjectsService
{
    public class EnrolledSubjectsService : IEnrolledSubjectsService
    {
        // Dependency injection
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // constructor to inject the DataContext again
        public EnrolledSubjectsService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<EnrolledSubjects>> GetAllEnrolledSubject()
        {
            var enrolled = await _context.EnrolledSubjects
                .Include(u => u.Subject)
                    .ThenInclude(u => u.Professors)
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
                        SubjectId = enrolledSub.SubjectId,
                        ProfessorId = enrolledSub.ProfessorId
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
            //get the current User UserId 
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //get the current User Role
            var userRole = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;

            //Initialized list of Enrolled Subjects
            List<EnrolledSubjects> subjects = new List<EnrolledSubjects>();

            // returns the Id of the student that is equal to the current logged in User
            var studentId = await _context.Students
                .Where(s => s.UserId.ToString() == userId)
                .Select (s => s.Id)
                .FirstOrDefaultAsync();

            if (userRole == "Student")
            {
                subjects = await _context.EnrolledSubjects
                   .Include(u => u.Enrollment)
                        .ThenInclude(u => u.Student)
                   .Include(u => u.Subject)
                        .ThenInclude(p => p.Professors)
                   .Where(u => u.Enrollment.StudentId == id && u.Enrollment.StudentId == studentId)
                   .ToListAsync();
            }

            else if (userRole == "Admin")
            {
                subjects = await _context.EnrolledSubjects
                   .Include(u => u.Enrollment)
                        .ThenInclude(u => u.Student)
                   .Include(u => u.Subject)
                        .ThenInclude(p => p.Professors)
                   .Where(u => u.Enrollment.StudentId == id)
                   .ToListAsync();

            }

            if (subjects == null)
                return null;
            return subjects;
        }   

        public async Task<List<EnrolledSubjects>> GetSingleEnrolledStudent(int id)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;
            List<EnrolledSubjects> subjects = new List<EnrolledSubjects>();

            var professorId = await _context.Professors
                .Where(s => s.UserId.ToString() == userId)
                .Select(s => s.Id)
                .FirstOrDefaultAsync();

            if (userRole == "Professor")
            {
                subjects = await _context.EnrolledSubjects
                    .Include(u => u.Enrollment)
                         .ThenInclude(u => u.Student)
                    .Include(u => u.Subject)
                         .ThenInclude(p => p.Professors)
                    .Where(u => u.ProfessorId == id && u.ProfessorId == professorId)
                    .ToListAsync();
            }

            else if (userRole == "Admin")
            {
                subjects = await _context.EnrolledSubjects
                        .Include(u => u.Enrollment)
                             .ThenInclude(u => u.Student)
                        .Include(u => u.Subject)
                             .ThenInclude(p => p.Professors)
                        .Where(u => u.ProfessorId == id)
                        .ToListAsync();
            }
            if (subjects == null)
                return null;
            return subjects;

        }

        //public async Task<List<EnrolledSubjects>> DeleteEnrolledSubject(int id)
        //{
        //    EnrolledSubjects enrolled = await _context.EnrolledSubjects.FindAsync(id);
        //    if (enrolled == null)
        //        return null;

        //    _context.EnrolledSubjects.
        //}
    }

}
