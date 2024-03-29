﻿using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Services.UserService
{
    public interface IUserService
    {
        Task<string> GetSingleUserId();
        Task<string> GetSingleUserName();

        Task<User?> GetSingleUser(int id);
        Task<List<User>?> DeleteUser(int id);
        Task<List<User>> GetAllUsers();
        Task<Student?> GetSingleStudent();
        Task<Professor?> GetSingleProfessor();
        Task<int> GetSingleProfessor(int id);
        Task<string> GetUserRole();
        Task<User> GetUserById(int userId);
        Task<User?> UpdateUser(int id, UserDetailsDTO request);
        Task<int> GetUserIdbyRole(int id, string role);
        Task<bool> UpdateStatus(string userEmail, bool status);
    }
}
