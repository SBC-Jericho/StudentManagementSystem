using Azure.Core;
using BlazorWasmDotnet8AspNetCoreHosted.Server.Services.UserService;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Hubs
{
    [Authorize]
    public class ChatHub : Hub<IChatClient>
    {

        public readonly static List<User> _Connections = new List<User>();
        private readonly static Dictionary<string, string> _ConnectionsMap = new Dictionary<string, string>();
        private readonly DataContext _dbContext;
        private readonly IUserService _userService;

        public ChatHub(DataContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }
        public async Task SendMessage(ChatMessage message, string userName) 
        {
            await Clients.All.ReceivedMessage(message, userName);
        }


        public async Task SendMessageToGroup(string groupName, GroupChatMessage message)
        {

            string name = IdentityName;
            Console.WriteLine(name);
            //var test = _IUserIdProvider.GetUserId();
            await Clients.All.ReceivedGroupMessage(message, groupName);
        }
        public async Task ChatNotificationAsync(string message, int receiverUserId, string senderUserId)
        {
            await Clients.All.ReceiveChatNotification(message, receiverUserId, senderUserId);
        }
        public async Task GroupChatNotificationAsync(string message, int senderId, int groupChatId)
        {
            await Clients.All.ReceiveGroupChatNotification(message, senderId, groupChatId);
        }
        public async Task RemoveMember(GroupChatMessage message, string userName) 
        {
            await Clients.All.RemoveMemberNotification(message, userName);   
        }

        public async Task CreateGroup(GroupChat groupChat)
        {
            await Clients.All.ReceiveNewGroupChat(groupChat);
        }
        public async Task DeleteGroup(int groupId)
        {
            await Clients.All.DeleteGroupChatHub(groupId);
        }

        public async Task GroupNameUpdated(int groupId, GroupChatNameDTO request)
        {
            await Clients.All.UpdateGroupName(groupId, request);
        }

        public async Task RemoveUserToGroup(string groupName, string userEmail)
        {
            string connectionIdToRemove = _ConnectionsMap.FirstOrDefault(x => x.Key == userEmail).Value;
            Console.WriteLine($" User to remove: {connectionIdToRemove}");
            var user = _dbContext.Users.Where(u => u.Email == userEmail).FirstOrDefault();
            if (user != null)
            {

                _Connections.Remove(user);

                // Tell other users to remove you from their list
                //Clients.OthersInGroup(user.CurrentRoom).SendAsync("removeUser", user);
               await Clients.Group(groupName).RemoveUser(userEmail);
                await Groups.RemoveFromGroupAsync(connectionIdToRemove, groupName);

                // Remove mapping

            }
        }

        public async Task AddToGroupChat(string groupName, User request)
        {
            await Clients.Group(groupName).AddUserToGroupChat(request);
        }

        //public async Task RemoveConnectionId(User user) 
        //{
        //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, user);
        //}

        //public async Task GetUserEmail() 
        //{
        //    await _userService.GetSingleUserName();
        //}
        public async Task Join(string groupName)
        {
            //userEmail = await _userService.GetSingleUserName();
            //try
            //{
            //    var user = _Connections.Where(u => u.Email == userEmail).FirstOrDefault();
            //    if (user == null && !_Connections.Any(u => u.Email == userEmail))
            //    {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            //        _Connections.Add(user);
            //        _ConnectionsMap.Add(userEmail, Context.ConnectionId);
            //        Console.WriteLine($"Welcome to the group{userEmail}");

            //    }

            //}
            //catch (Exception ex)
            //{
            //    await Clients.Caller.onError("You failed to join the chat room!" + ex.Message);
            //}
        }

        public Task Leave(string groupName)   
        {
           return Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        //public async Task UpdateUserStatus(string email) 
        //{
        //    await Clients.All.UpdateStatus(email);
        //}

        public async Task UpdateStatus(string email, bool status)
        {
            await Clients.All.UpdateUserStatus(email, status);
        }

        public async Task CountMessages(int senderId)
        {
            await Clients.All.CountOtherUserMessage(senderId);
        }

        public async Task UserToRemove(string groupName, string connectionId)
        {
            await Groups.RemoveFromGroupAsync(connectionId, groupName);
        }
        public override async Task OnConnectedAsync()
        {
            try
            {
                  
                var Email = IdentityName;
                await _userService.UpdateStatus(Email, true);
                await Clients.All.UpdateUserStatus(Email, true);

                var user = _dbContext.Users.Where(u => u.Email == IdentityName).FirstOrDefault();

                if (!_ConnectionsMap.ContainsKey(user.Email) && user != null)
                {
                    _Connections.Add(user);
                    _ConnectionsMap.Add(Email, Context.ConnectionId);

                    Console.WriteLine(Email);
                    Console.WriteLine($"User connected with ConnectionId: {Context.ConnectionId}");
                    Console.WriteLine($"Connections: {_ConnectionsMap.Count}");
                }

                //await Clients.All.ReceiveGroupToUpdate();
            }
            catch (Exception ex)
            {
                //await Clients.Caller.SendAsync("onError", "OnConnected: " + ex.Message);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                var userEmail = IdentityName;
                await _userService.UpdateStatus(userEmail, false);
                await Clients.All.UpdateUserStatus(userEmail, false);
                User? user = _Connections.Where(u => u.Email == userEmail).First();
                _Connections.Remove(user);

                // Tell other users to remove you from their list
                //Clients.OthersInGroup(user.CurrentRoom).SendAsync("removeUser", user);

                // Remove mapping
                _ConnectionsMap.Remove(userEmail);
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"{exception.Message}");  
            }

            await base.OnDisconnectedAsync(exception);
        }


        private string IdentityName
        {
            get { return Context.User.Identity.Name; }
        }

    }
}
