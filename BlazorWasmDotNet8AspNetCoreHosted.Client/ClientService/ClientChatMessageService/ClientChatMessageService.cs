using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;

namespace BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientChatMessageService
{
    public class ClientChatMessageService : IClientChatMessageService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public ClientChatMessageService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public async Task<List<User>> GetAllUsers()
        {
                var result = await _http.GetFromJsonAsync<List<User>>($"api/chatmessage/users");
            return result;
        }

        //public async Task<List<ChatMessage>> GetConversationAsync(int contactId)
        //{
        //    return await _httpClient.GetFromJsonAsync<List<ChatMessage>>($"api/Chat/get-chat/{contactId}");
        //}
        public async Task<List<ChatMessage>> GetConversation(int receiverId)
        {
          
            return await _http.GetFromJsonAsync<List<ChatMessage>>($"api/chatmessage/get-conversation/{receiverId}");
           
        }

        public async Task<User?> GetSingleUser(int userId)
        {
            var result =  await _http.GetAsync($"api/chatmessage/single-user/{userId}");

            if (result.StatusCode == HttpStatusCode.OK) 
            {
                return await result.Content.ReadFromJsonAsync<User>();
            }

            return null;

        }

        public async Task SaveMessage(ChatMessage message)
        {
            await _http.PostAsJsonAsync("api/chatmessage", message);
        }
    }
}
