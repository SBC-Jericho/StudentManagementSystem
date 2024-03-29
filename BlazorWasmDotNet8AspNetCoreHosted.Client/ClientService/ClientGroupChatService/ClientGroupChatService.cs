﻿using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
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
        public async Task<User> AddUserToGroup(AddUserToGroupDTO request)
        {
            
                var response = await _http.PostAsJsonAsync("api/GroupChat/add-user-to-group/", request);

                if (response.IsSuccessStatusCode)
                {
                    var addedUser = await response.Content.ReadFromJsonAsync<User>();

                    _snackbar.Add(
                        "Successfully Added User to Group",
                        Severity.Success,
                        config =>
                        {
                            config.ShowTransitionDuration = 200;
                            config.HideTransitionDuration = 400;
                            config.VisibleStateDuration = 2500;
                        });

                    return addedUser;
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
                    return null;
                }
           
        }

        public async Task DeleteGroupChat(int id)
        {
            var result = await _http.DeleteAsync($"api/Groupchat/delete-group/{id}");


            if (result.IsSuccessStatusCode)
            {
                List<GroupChat>? content = await result.Content.ReadFromJsonAsync<List<GroupChat>>();
                if (content != null) clientGroupChat = content;
            }
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
        public async Task<bool> CheckIfMemebr(int groupId)
        {
            var result = await _http.GetAsync($"api/GroupChat/check-if-member/{groupId}");

            if (result.IsSuccessStatusCode)
            {

                return true;
            }
            else
            {
                _snackbar.Add(
                   "You are not part of this group",
                   Severity.Warning,
                   config =>
                   {
                       config.ShowTransitionDuration = 200;
                       config.HideTransitionDuration = 400;
                       config.VisibleStateDuration = 2500;
                   });
                return false;
            }


        }

        public async Task<GetChatMembersDTO> GetGroupChatMembers(int groupChatId)
        {
            var result = await _http.GetAsync($"api/GroupChat/users-from-group/{groupChatId}");

            if (result.IsSuccessStatusCode)
            {
                var users = await result.Content.ReadFromJsonAsync<GetChatMembersDTO>();
                return users;
            }

            return new GetChatMembersDTO();
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


         public async Task<List<GroupChatMessage>> GetGroupChatConversation(int receiverId)
        {
            return await _http.GetFromJsonAsync<List<GroupChatMessage>>($"api/GroupChat/get-groupchat-conversation/{receiverId}");
        }
        public async Task<bool> SaveMessage(GroupChatMessage message)
        {
            var result = await _http.PostAsJsonAsync("api/GroupChat/save-group-message", message);
            if (result.IsSuccessStatusCode)
            {
               
                return true;
            }
            else
            {
                _snackbar.Add(
                   "You are not part of this group",
                   Severity.Warning,
                   config =>
                   {
                       config.ShowTransitionDuration = 200;
                       config.HideTransitionDuration = 400;
                       config.VisibleStateDuration = 2500;
                   });
                return false;
            }

        }

        public async Task<bool> CheckIfGroupMember(int groupId)
        {
            var result = await _http.GetAsync("api/GroupChat/check-if-group-member");
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task UpdateGroupChat(int id, GroupChatNameDTO request)
        {
            await _http.PutAsJsonAsync($"api/Groupchat/update-single-group/{id}", request);
        }
    }
}
