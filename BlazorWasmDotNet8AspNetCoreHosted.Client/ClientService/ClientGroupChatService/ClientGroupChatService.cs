using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientGroupChatService
{
    public class ClientGroupChatService : IClientGroupChatService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public ClientGroupChatService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public List<GroupChat> clientGroupChat { get; set ; } = new List<GroupChat>();

        public async Task AddGroupChat(GroupChatDTO request)
        {
            // Controller end point
            var result = await _http.PostAsJsonAsync("api/groupchat/", request);

            if (result.IsSuccessStatusCode)
            {
                List<GroupChat>? content = await result.Content.ReadFromJsonAsync<List<GroupChat>>();
                if (content != null) clientGroupChat = content;
            }
        }

        public Task<bool> AddUserToGroup(int userId, int groupChatId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteGroup(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GroupChat>> GetAllGroup()
        {
            var result = await _http.GetFromJsonAsync<List<GroupChat>>("api/GroupChat/get-all-group/");
            return result;
          
        }

        public async Task<List<GroupChat>> GetAllGroupsForUser()
        {
                var result = await _http.GetFromJsonAsync<List<GroupChat>>("api/groupchat/get-group-single-user/");
                return result;
            
        }

        public async Task<List<GroupChatMessage>> GetGroupChatConversation(int receiverId)
        {
            return await _http.GetFromJsonAsync<List<GroupChatMessage>>($"api/groupchat/get-groupchat-conversation/{receiverId}");
        }

        public async Task<List<User>> GetGroupMembers(int groupId)
        {
            var result = await _http.GetFromJsonAsync<List<User>>($"api/GroupChat/get-group-members/{groupId}");

            return result;

        } 
        public async Task<List<User>> GetNotMembers(int groupId)
        {
            var result = await _http.GetFromJsonAsync<List<User>>($"api/GroupChat/get-notgroup-members/{groupId}");

            return result;

        }

        public async Task<GroupChat?> GetSingleGroup(int Id)
        {
            var result = await _http.GetAsync($"api/groupchat/single-group/{Id}");

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<GroupChat>();
            }

            return null;
        }

        public async Task<string> GetSingleGroupName(int id)
        {
            var result = await _http.GetStringAsync($"api/User/single-group-name/{id}");
            return result;
        }


        public Task<bool> RemoveUserToGroup(int userId, int groupChatId)
        {
            throw new NotImplementedException();
        }

        public async Task SaveMessage(GroupChatMessage message)
        {
            await _http.PostAsJsonAsync("api/groupchat/save-group-message", message);
        }
    }
}
