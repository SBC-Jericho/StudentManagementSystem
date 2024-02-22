using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
using System.Net.Http.Json;
using System.Net;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Components;

namespace BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientUserService
{
    public class ClientUserService : IClientUserService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public ClientUserService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }
        public List<User> Users { get; set; } = new List<User>();

        public async Task DeleteUser(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/auth/{id}");


            if (result.IsSuccessStatusCode)
            {
                List<User>? content = await result.Content.ReadFromJsonAsync<List<User>>();
                if (content != null) Users = content;
            }
        }

        public async Task<List<User>> GetAllUser()
        {
            var result = await _httpClient.GetFromJsonAsync<List<User>>($"api/User/");
            if (result != null)
            {
                Users = result;
            }

            return Users;
        }

        public async Task<User?> GetSingleUser(int id)
        {
            var result = await _httpClient.GetAsync($"api/User/get-single-user/{id}");
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<User>();
            }

            return null;
        }

        public async Task<string> GetSingleUserId()
        {
            var result = await _httpClient.GetStringAsync("api/User/single-user-id");
            return result;
        }
        public async Task<User> GetUserById(int id)
        {
            var result = await _httpClient.GetAsync($"api/User/get-user-byId/{id}");
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<User>();
            }
            return null;
        }

        public async Task<string> GetSingleUserName()
        {
            var result = await _httpClient.GetStringAsync("api/User/single-user-name");
            return result;
        }

        public async Task<Student?> GetSingleStudent()
        {
            var result = await _httpClient.GetAsync($"api/User/single-student");
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<Student>();
            }
            return null;
        }

        public async Task<Professor?> GetSingleProfessor()
        {
            var result = await _httpClient.GetAsync($"api/User/single-professor");
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<Professor>();
            }
            return null;
        }
        public async Task<int> GetSingleProfessor(int id)
        {
            var result = await _httpClient.GetAsync($"api/User/professor-id/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<int>();
            }
            return 0;
        }

        public async Task<string> GetUserRole()
        {
            var result = await _httpClient.GetStringAsync("api/User/user-role");
            return result;
        }

        public async Task<int> GetUserIdbyRole(int id, string role)
        {
            var result = await _httpClient.GetAsync($"api/User/user-id-by-Role?id={id}&role={role}");
            if (result.IsSuccessStatusCode)
            {
                var userId = await result.Content.ReadFromJsonAsync<int>();
                return userId;
            }
            return 5;
        }

        public async Task UpdateUser(int id, User request)
        {
            await _httpClient.PutAsJsonAsync($"api/user/update-user/{id}", request);
            _navigationManager.NavigateTo("all-user");
        }

        public async Task<bool> UpdateStatus(string userEmail, bool status)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/user/update-user-status/", userEmail);
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;

        }
    }
}
