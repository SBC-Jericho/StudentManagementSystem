using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Json;

namespace BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientStudentService
{
    public class ClientStudentService : IClientStudentService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public ClientStudentService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public List<Student> ClientStudent { get; set; } = new List<Student>();

        public async Task AddStudent(Student student)
        {
            // Controller end point
            await _http.PostAsJsonAsync("api/student/", student);
            // Razor page endpoint
            _navigationManager.NavigateTo("all-student");
        }

        public async Task DeleteStudent(int id)
        {
            var result = await _http.DeleteAsync($"api/student/{id}");


            if (result.IsSuccessStatusCode)
            {
                List<Student>? content = await result.Content.ReadFromJsonAsync<List<Student>>();
                if (content != null) ClientStudent = content;
            }
        }

        public async Task<List<Student>> GetAllStudent()
        {
            var result = await _http.GetFromJsonAsync<List<Student>>("api/student/");
            if (result != null)
            {
                ClientStudent = result;
            }
            return ClientStudent;
        }

        public async Task<Student?> GetSingleStudent(int id)
        {
            // if provided an Id that does not exist GetAsync returns null
            var result = await _http.GetAsync($"api/student/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<Student>();
            }
            return null;
        }

        public async Task UpdateStudent(int id, Student request)
        {
            await _http.PutAsJsonAsync($"api/student/{id}", request);
            _navigationManager.NavigateTo("all-student");
        }
    }
}
