using BlazorWasmDotnet8AspNetCoreHosted.Server.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }
        [HttpGet("single-user-id")]
        public async Task<ActionResult<string>> GetSingleUserId()
        {
            var result = await _userService.GetSingleUserId();

            return Ok(result);
        }

        [HttpGet("single-user-name")]
        public async Task<ActionResult<string>> GetSingleUserName()
        {
            var result = await _userService.GetSingleUserName();

            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            if (result is null)
            {
                return NotFound("User not found");
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var result = await _userService.GetAllUsers();
            return Ok(result);
        }

        [HttpGet("single-student")]
        public async Task<ActionResult<Student>?> GetSingleStudent()
        {
            var result = await _userService.GetSingleStudent();
            if (result is null)
                return NotFound();
            return Ok(result);
        } 
        [HttpGet("single-professor")]
        public async Task<ActionResult<Professor>?> GetSingleProfessor()
        {
            var result = await _userService.GetSingleProfessor();
            if (result is null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("professor-id/{id}")]
        public async Task<ActionResult<int>> GetSingleProfessor(int id)
        {
            var result = await _userService.GetSingleProfessor(id);
            //if (result is null)
            //    return NotFound("Student not found.");
            return Ok(result);
        }

        [HttpGet("user-role")]
        public async Task<ActionResult<string>> GetUserRole()
        {
            var result = await _userService.GetUserRole();

            return Ok(result);
        }
    }
}
