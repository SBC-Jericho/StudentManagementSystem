using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Net;

namespace BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientProfessorService
{
    public class ClientProfessorService : IClientProfessorService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public ClientProfessorService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public List<Professor> ClientProfessor { get; set; } = new List<Professor>();

        public async Task AddProfessor(Professor Professor)
        {
            // Controller end point
            await _http.PostAsJsonAsync("api/Professor/", Professor);
            // Razor page endpoint
            _navigationManager.NavigateTo("all-professor");
        }

        public async Task DeleteProfessor(int id)
        {
            var result = await _http.DeleteAsync($"api/Professor/{id}");


            if (result.IsSuccessStatusCode)
            {
                List<Professor>? content = await result.Content.ReadFromJsonAsync<List<Professor>>();
                if (content != null) ClientProfessor = content;
            }
        }

        public async Task<List<Professor>> GetAllProfessor()
        {
            var result = await _http.GetFromJsonAsync<List<Professor>>("api/Professor/");
            if (result != null)
            {
                ClientProfessor = result;
            }
            return ClientProfessor;
        }

        public async Task<Professor?> GetSingleProfessor(int id)
        {
            // if provided an Id that does not exist GetAsync returns null
            var result = await _http.GetAsync($"api/Professor/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<Professor>();
            }
            return null;
        }

        public async Task UpdateProfessor(int id, Professor request)
        {
            await _http.PutAsJsonAsync($"api/Professor/{id}", request);
            _navigationManager.NavigateTo("all-professor");
        }
    }
}
