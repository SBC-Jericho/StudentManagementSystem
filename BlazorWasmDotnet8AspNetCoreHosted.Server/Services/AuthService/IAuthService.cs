using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<User> Register(userDTO request);
        Task<string> Login(userLoginDTO request);
        Task<User?> GetSingleUser(int id);
        Task<List<User>> GetAllUser();
        Task<string> GetSingleUserAvatar();
        Task<List<User>?> DeleteUser(int id);
    }
}
