namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Services.UserService
{
    public interface IUserService
    {
        Task<string> GetSingleUserId();
        Task<string> GetSingleUserName();
        Task<List<User>?> DeleteUser(int id);
        Task<List<User>> GetAllUsers();
        Task<Student?> GetSingleStudent();
        Task<Professor?> GetSingleProfessor();
        Task<int> GetSingleProfessor(int id);
        Task<string> GetUserRole();
        Task<User> GetUserById(int userId);
    }
}
