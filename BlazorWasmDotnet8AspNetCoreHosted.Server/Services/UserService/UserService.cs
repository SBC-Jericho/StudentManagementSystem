using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<User>?> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null)
                return null;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return await _context.Users.ToListAsync();
        }

        public async Task<string> GetSingleUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var users = await _context.Users    
                     .Where(p => p.Id.ToString() == userId)
                      .Select(p => p.Id.ToString())
                     .FirstOrDefaultAsync();

            return users;
        }

        public async Task<User> GetUserById(int userId)
        {
            
            var users = await _context.Users
                     .Where(p => p.Id == userId)
                     .FirstOrDefaultAsync();

            return users;
        }

        public async Task<string> GetSingleUserName()
        {
            var userEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value;

            var users = await _context.Users
                     .Where(p => p.Email == userEmail)
                      .Select(p => p.Email)
                     .FirstOrDefaultAsync();

            return users;
        }
        public async Task<List<User>> GetAllUsers()
        {
            var users = await _context.Users
               .Include(p => p.Students)
               .Include(p => p.Professor)
               .ToListAsync();
            return users;
        }

        public async Task<int> GetUserIdbyRole(int id, string role) 
        {
            if (role == "Student") 
            {
                //select studentId
                var studentId = await _context.Students
                    .Where(s => s.UserId == id)
                    .Select(p => p.Id)
                    .FirstOrDefaultAsync();
                if (studentId != 0)
                {
                    return studentId;

                }
            }

            else if (role == "Professor")
            {
                //select studentId
                var professorId = await _context.Professors
                    .Where(s => s.UserId == id)
                    .Select(p => p.Id)
                    .FirstOrDefaultAsync();
                if (professorId != 0)
                {
                    return professorId;

                }
            }

            return 0;

        }
        public async Task<User?> UpdateUser(int id, UserDetailsDTO request)
        {
            var user = await _context.Users
                .Include(u => u.Students)
                .Include (u => u.Professor)
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
            if (user is null)
                return null;
            if (user.Role == "Student") 
            {
                user.Students.FirstName = request.FirstName;
                user.Students.LastName = request.LastName;
                user.Students.BirthDate = request.BirthDate;
                user.Students.Contact = request.Contact;
                user.Students.Address = request.Address;
                user.Students.Image = request.Image;
                
            }

            if (user.Role == "Professor") 
            {
                user.Professor.FirstName = request.FirstName;
                user.Professor.LastName = request.LastName;
                user.Professor.BirthDate = request.BirthDate;
                user.Professor.Contact = request.Contact;
                user.Professor.Address = request.Address;
                user.Professor.Image = request.Image;
            }
         
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> GetSingleUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null)
                return null;
            return user;
        }

        public async Task<Student?> GetSingleStudent()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId.IsNullOrEmpty()) return null;

            var student = await _context.Users
                      .Where(p => p.Id.ToString() == userId)
                      .Include(p => p.Students)
                      .Select(p => p.Students)
                      .FirstOrDefaultAsync();

            return student;
        } 
        public async Task<Professor?> GetSingleProfessor()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId.IsNullOrEmpty()) return null;

            var professor = await _context.Users
                      .Where(p => p.Id.ToString() == userId)
                      .Include(p => p.Professor)
                      .Select(p => p.Professor)
                      .FirstOrDefaultAsync();

            return professor;
        }

        public async Task<int> GetSingleProfessor(int id)
        {
            var professor = await _context.Professors
                    .Where(p => p.UserId == id)
                    .Select(p => p.Id)
                    .FirstOrDefaultAsync();

            return professor;
        }

        public async Task<string> GetUserRole()
        {
            var userRole = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;

            var users = await _context.Users
                    .Where(p => p.Role == userRole)
                     .Select(p => p.Role)
                    .FirstOrDefaultAsync();

            return users;
        }

        public async Task<bool> UpdateStatus(string userEmail, bool newStatus)
        {
            var user = await _context.Users
                .Where(u => u.Email == userEmail)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                user.ActiveStatus = newStatus;
                await _context.SaveChangesAsync();
            }

            return user.ActiveStatus;
        }

    }
}
