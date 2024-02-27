using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;

namespace BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientUserService
{
    public interface IClientUserService
    {   
        List<User> Users { get; set; }
        Task DeleteUser(int id);
        Task<List<User>> GetAllUser();
        Task<string> GetSingleUserId();
        Task<User> GetUserById(int id);
        Task<string> GetSingleUserName();
        Task<Student?> GetSingleStudent();
        Task<Professor?> GetSingleProfessor();
        Task<int> GetSingleProfessor(int id);
        Task<string> GetUserRole();
        Task<User?> GetSingleUser(int id);
        Task UpdateUser(int id, User request);
        Task<bool> UpdateStatus(string userEmail, bool status);
        Task<int> GetUserIdbyRole(int id, string role);
    }
}
