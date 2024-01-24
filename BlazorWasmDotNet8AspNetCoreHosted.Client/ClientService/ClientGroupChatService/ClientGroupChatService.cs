using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
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
        private readonly ISnackbar _snackbar;


        public ClientGroupChatService(HttpClient http, NavigationManager navigationManager, ISnackbar snackbar)
        {
            _http = http;
            _navigationManager = navigationManager;
            _snackbar = snackbar;
        }
        public List<GroupChat> clientGroupChat { get; set ; } = new List<GroupChat>();
        public List<User> clientUser { get; set ; } = new List<User>();

        public async Task AddGroupChat(GroupChatDTO request)
        {
            // Controller end point
            var result = await _http.PostAsJsonAsync("api/GroupChat/", request);

            if (result.IsSuccessStatusCode)
            {
                List<GroupChat>? content = await result.Content.ReadFromJsonAsync<List<GroupChat>>();
                if (content != null) clientGroupChat = content;
            }
        }
        public async Task<GroupChat> AddUserToGroup(AddUserToGroupDTO request)
        {
            
                var response = await _http.PostAsJsonAsync("api/GroupChat/add-user-to-group/", request);

                if (response.IsSuccessStatusCode)
                {
                    var addedGroup = await response.Content.ReadFromJsonAsync<GroupChat>();

                    _snackbar.Add(
                        "Successfully Added User to Group",
                        Severity.Success,
                        config =>
                        {
                            config.ShowTransitionDuration = 200;
                            config.HideTransitionDuration = 400;
                            config.VisibleStateDuration = 2500;
                        });

                    return addedGroup;
                }              
                else
                {
                    _snackbar.Add(
                        "Failed to add user to the group",
                        Severity.Error,
                        config =>
                        {
                            config.ShowTransitionDuration = 200;
                            config.HideTransitionDuration = 400;
                            config.VisibleStateDuration = 2500;
                        });
                }
            return null;
           
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
                var result = await _http.GetFromJsonAsync<List<GroupChat>>("api/GroupChat/get-group-single-user/");
                return result;
            
        }

        public async Task<List<User>> GetAllUsersExceptCurrent()
        {
            var result = await _http.GetFromJsonAsync<List<User>>("api/GroupChat/get-all-user-except/");

            if (result != null)
            {
                // Assuming there's a property like Id in the User class
                clientUser = result;
            }

            return clientUser;
        }

        public async Task<List<GroupChatMessage>> GetGroupChatConversation(int receiverId)
        {
            return await _http.GetFromJsonAsync<List<GroupChatMessage>>($"api/GroupChat/get-groupchat-conversation/{receiverId}");
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
            var result = await _http.GetAsync($"api/GroupChat/single-group/{Id}");

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<GroupChat>();
            }

            return null;
        }

        public async Task<User?> GetSingleUser(int id)
        {
            // if provided an Id that does not exist GetAsync returns null
            var result = await _http.GetAsync($"api/GroupChat/single-group-user/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<User>();
            }
            return null;
        }

        public async Task<string> GetSingleGroupName(int id)
        {
            var result = await _http.GetStringAsync($"api/GroupChat/single-group-name/{id}");
            return result;
        }

        public async Task RemoveUserToGroup(int userId, int groupChatId)
        {
            await _http.DeleteAsync($"api/GroupChat/remove-user-to-group/{userId}/{groupChatId}");
        }

        public async Task SaveMessage(GroupChatMessage message)
        {
            await _http.PostAsJsonAsync("api/GroupChat/save-group-message", message);
        }
    }
}
