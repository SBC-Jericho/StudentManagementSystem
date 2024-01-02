using BlazorWasmDotnet8AspNetCoreHosted.Server.Data;
using BlazorWasmDotnet8AspNetCoreHosted.Server.Services.AuthService;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public AuthController(IConfiguration configuration, IAuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }

        [HttpGet("single-user-avatar")]
        public async Task<ActionResult<string>> GetSingleUserAvater()
        {
            var result = await _authService.GetSingleUserAvatar();

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(userDTO request)
        {

            var result = await _authService.Register(request);
            return Ok(result);
        }



        [HttpPost("login")]

        public async Task<ActionResult<string>> Login(userLoginDTO request)
        {
            var result = await _authService.Login(request);
            if (result == "No User Found" || result == "Wrong password.")
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUser()
        {

            return await _authService.GetAllUser();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteUser(int id)
        {
            var result = await _authService.DeleteUser(id);
            if (result is null)
                return NotFound("User not Found");

            return Ok(result);
        }
    }
}
