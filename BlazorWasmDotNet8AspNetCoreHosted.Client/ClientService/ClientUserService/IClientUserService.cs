using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;

namespace BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientUserService
{
    public interface IClientUserService
    {
        List<User> Users { get; set; }
        Task DeleteUser(int id);
        Task GetAllUser();
        Task<string> GetSingleUserAvatar();
        Task<Student?> GetSingleStudent();
        Task<Professor?> GetSingleProfessor();
        Task<int> GetSingleProfessor(int id);
        Task<string> GetUserRole();
    }
}
