using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Net.NetworkInformation;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {

        public readonly static List<User> _Connections = new List<User>();
        private readonly static Dictionary<string, string> _ConnectionsMap = new Dictionary<string, string>();
        private readonly DataContext _dbContext;

        private readonly IUserIdProvider _IUserIdProvider;
        public ChatHub(DataContext dbContext)
        { 

            _dbContext = dbContext;

        }

        public async Task SendMessage(ChatMessage message, string userName) 
        {
            await Clients.All.ReceivedMessage(message, userName);
        }

        public async Task ChatNotificationAsync(string message, int receiverUserId, string senderUserId)
        {
            await Clients.All.ReceiveChatNotification(message, receiverUserId, senderUserId);
        }
        public async Task GroupChatNotificationAsync(string message, int receiverUserId, int groupChatId)
        {
            await Clients.All.ReceiveGroupChatNotification(message, receiverUserId, groupChatId);
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
        public async Task RemoveUserToGroup(string groupName, int userId)
        {
            await Clients.Group(groupName).RemoveUser(userId);
        }

        public async Task AddToGroupChat(string groupName, User request)
        {
            await Clients.Group(groupName).AddUserToGroupChat(request);
        }
        public async Task Join(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public Task Leave(string groupName)   
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendMessageToGroup(string groupName, GroupChatMessage message)
        {

            string name = Context.User.Identity.Name;
            Console.WriteLine(name);
            //var test = _IUserIdProvider.GetUserId();
            await Clients.Group(groupName).ReceivedGroupMessage(message);
        }

        //public override async Task OnConnectedAsync()
        //{
        //    try
        //    {
        //        User? user = _dbContext.Users.Where(u => u.Email == IdentityName).FirstOrDefault();
        //        if (user != null)
        //        {
        //            User userViewModel = new User
        //            {
        //                Email = IdentityName
        //            };


        //            if (!_Connections.Any(u => u.Email == IdentityName))
        //            {
        //                _Connections.Add(userViewModel);
        //                _ConnectionsMap.Add(IdentityName, Context.ConnectionId);
        //            }

        //            Clients.Caller.SendAsync("getProfileInfo", userViewModel);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Clients.Caller.SendAsync("onError", "OnConnected:" + ex.Message);
        //    }
        //    return base.OnConnectedAsync();
        //}

        //private string IdentityName
        //{
        //    get { return Context.User.Identity.Name; }
        //}

    }
}
