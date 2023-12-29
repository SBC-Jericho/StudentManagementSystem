using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;

namespace BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientAuthService
{
    public class ClientAuthService : IClientAuthService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public List<User> ClientUser { get; set; } = new List<User>();
        public Token token { get; set; } = new Token();

        public ClientAuthService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public async Task GetAllUser()
        {
            var result = await _http.GetFromJsonAsync<List<User>>("api/auth/");
            if (result != null)
            {
                ClientUser = result;
            }
        }

        //public async Task<string> GetSingleUserAvatar()
        //{
        //    var result = await _http.GetStringAsync("api/Auth/single-avatar");
        //    return result;
        //}

        public async Task DeleteUser(int id)
        {
            var result = await _http.DeleteAsync($"api/auth/{id}");


            if (result.IsSuccessStatusCode)
            {
                List<User>? content = await result.Content.ReadFromJsonAsync<List<User>>();
                if (content != null) ClientUser = content;
            }
        }

        public async Task<string> Login(userLoginDTO request)
        {

            var result = await _http.PostAsJsonAsync("api/Auth/login", request);

            var data = await SetToken(result);

            return data;
        }
        public async Task Register(userDTO request)
        {
            // Controller end point
            await _http.PostAsJsonAsync("api/auth/register", request);
            // Razor page endpoint
            _navigationManager.NavigateTo("all-characters");
        }

        private async Task<string> SetToken(HttpResponseMessage message)
        {
            if (message.IsSuccessStatusCode)
            {
                token.value = await message.Content.ReadAsStringAsync();
                return "success";
            }
            else
            {
                return await message.Content.ReadAsStringAsync();
            }
        }

    }
}
