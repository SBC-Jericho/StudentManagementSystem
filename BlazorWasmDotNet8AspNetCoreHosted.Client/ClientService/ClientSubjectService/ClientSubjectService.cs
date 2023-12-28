using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Net;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;

namespace BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientSubjectService
{
    public class ClientSubjectService : IClientSubjectService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public ClientSubjectService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public List<Subject> ClientSubject { get; set; } = new List<Subject>();

        public async Task AddSubject(SubjectDTO subject)
        {
            // Controller end point
            await _http.PostAsJsonAsync("api/Subject/", subject);
            // Razor page endpoint
            _navigationManager.NavigateTo("all-subject");
        }

        public async Task DeleteSubject(int id)
        {
            var result = await _http.DeleteAsync($"api/Subject/{id}");


            if (result.IsSuccessStatusCode)
            {
                List<Subject>? content = await result.Content.ReadFromJsonAsync<List<Subject>>();
                if (content != null) ClientSubject = content;
            }
        }

        public async Task<List<Subject>> GetAllSubject()
        {
            var result = await _http.GetFromJsonAsync<List<Subject>>("api/Subject/");
            if (result != null)
            {
                ClientSubject = result;
            }
            return ClientSubject;
        }

        public async Task<Subject?> GetSingleSubject(int id)
        {
            // if provided an Id that does not exist GetAsync returns null
            var result = await _http.GetAsync($"api/Subject/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<Subject>();
            }
            return null;
        }

        public async Task UpdateSubject(int id, Subject request)
        {
            await _http.PutAsJsonAsync($"api/Subject/{id}", request);
            _navigationManager.NavigateTo("all-subject");
        }
    }
}
