using BlazorWasmDotnet8AspNetCoreHosted.Server.Data;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        //constructor to inject the DataContext again
        public static User user = new User();
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration, DataContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        public async Task<List<User>> GetAllUser()
        {
            var users = await _context.Users
              .Include(c => c.Students)
              .Include(c => c.Professor)
              .ToListAsync();
            return users;
        }

        public async Task<string> Login(userLoginDTO request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.Email == request.Email.Trim());

            if (user == null)
            {
                return "No User Found";
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return "Wrong password.";
            }

            var token = CreateToken(user);

            return token;
        }

        public async Task<User> Register(userDTO request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User new_user = new User
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = request.Role
            };

            if (request.Role == "Student") 
            {
                Student student_details = new Student
                {
                    FirstName = request.userDetailsDTO.FirstName,
                    LastName = request.userDetailsDTO.LastName,
                    Contact = request.userDetailsDTO.Contact,
                    Address = request.userDetailsDTO.Address,
                    Image = request.userDetailsDTO.Image,
                    BirthDate = request.userDetailsDTO.BirthDate,
                    User = new_user,
                    UserId = new_user.Id
                };

                _context.Students.Add(student_details);
            }
            else if (request.Role == "Professor")
            {
                Professor professor_details = new Professor
                {
                    FirstName = request.userDetailsDTO.FirstName,
                    LastName = request.userDetailsDTO.LastName,
                    Contact = request.userDetailsDTO.Contact,
                    Address = request.userDetailsDTO.Address,
                    Image = request.userDetailsDTO.Image,
                    BirthDate = request.userDetailsDTO.BirthDate,
                    User = new_user,
                    UserId = new_user.Id
                };

                _context.Professors.Add(professor_details);
            }

            _context.Users.Add(new_user);
            await _context.SaveChangesAsync();

            return new_user;

        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
    {
        new Claim(ClaimTypes.Name, user.Email),
        new Claim(ClaimTypes.Role, user.Role)
    };

            // 403 Don't have the Correct Role 
            // 401 No Autherization Header Set

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken
                (
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

    }
}
