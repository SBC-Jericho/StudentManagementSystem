using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Services.StudentService
{
    public class StudentService : IStudentService
    {
       // Dependency injection
        private readonly DataContext _context;

       // constructor to inject the DataContext again
        public StudentService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Student>> AddStudent(UserDetailsDTO stud)
        {
            Student newStudent = new Student
            {
                FirstName = stud.FirstName,
                LastName = stud.LastName,
                BirthDate = stud.BirthDate,
                Contact = stud.Contact,
                Address = stud.Address,
                Image = stud.Image,
            };

            _context.Add(newStudent);
            await _context.SaveChangesAsync();
            return await _context.Students.ToListAsync();
        }

        public async Task<List<Student>?> DeleteStudent(int id)
        {
            var stud = await _context.Students.FindAsync(id);
            if (stud is null)
                return null;

            _context.Students.Remove(stud);
            await _context.SaveChangesAsync();


            return await _context.Students.ToListAsync();
        }

        public async Task<List<Student>> GetAllStudent()
        {
            var student = await _context.Students
                .Include(u => u.User)
                .ToListAsync();
            return student;
        }

        public async Task<Student?> GetSingleStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student is null)
                return null;
            return student;
        }

        public async Task<List<Student>?> UpdateStudent(int id, UserDetailsDTO request)
        {

            var student = await _context.Students
                .Include(u => u.User)   
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
            if (student is null)
                return null;

            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            student.BirthDate = request.BirthDate;
            student.Contact = request.Contact;
            student.Address = request.Address;
            student.Image = request.Image;
            student.User.Avatar = request.Image;

            await _context.SaveChangesAsync();

            return await _context.Students.ToListAsync();
        }
    }
}
