using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
using System.Runtime.InteropServices;

namespace BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientAuthService
{
    public interface IClientAuthService
    {
        List<User> ClientUser { get; set; }
        Token token { get; set; }
        Task Register(userDTO request);
        Task<string> Login(userLoginDTO request);
        Task GetAllUser();
        Task DeleteUser(int id);
        Task<string> GetSingleUserAvatar();
        //Task<User?> GetSingleUser(int id);
        //Task<string> GetSingleUser();

    }
}
