﻿namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Services.UserService
{
    public interface IUserService
    {
        Task<string> GetSingleUser();
        Task<List<User>?> DeleteUser(int id);
        Task<List<User>> GetAllUsers();
        Task<Student?> GetSingleStudent();
        Task<Professor?> GetSingleProfessor();
        Task<int> GetSingleProfessor(int id);
        Task<string> GetUserRole();
    }
}
