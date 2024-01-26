using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Hubs
{

    public class ChatHub : Hub
    {
      

        public async Task SendMessage(ChatMessage message, string userName)
          {
            
                await Clients.All.SendAsync("ReceivedMessage", message, userName);
          }

         public async Task ChatNotificationAsync(string message, int receiverUserId, string senderUserId)
         {
                await Clients.All.SendAsync("ReceiveChatNotification", message, receiverUserId, senderUserId);
         }

        public async Task GroupChatNotificationAsync(string message, int receiverUserId, int groupChatId)
        {
            await Clients.All.SendAsync("ReceiveGroupChatNotification", message, receiverUserId, groupChatId);
        }

        public async Task CreateGroup(GroupChat groupChat) 
        {
            await Clients.All.SendAsync("ReceiveNewGroupChat", groupChat);
        }
        public async Task DeleteGroup(int groupId) 
        {
            await Clients.All.SendAsync("DeleteGroupChatHub", groupId);
        }

        public async Task GroupNameUpdated(int groupId, GroupChatNameDTO request)
        {
            await Clients.All.SendAsync("UpdateGroupName", groupId, request);
        }
        public async Task RemoveUserToGroup(string groupName,  int userId) 
        {
            await Clients.Group(groupName).SendAsync("RemoveUser", userId);
        }

        public async Task AddToGroupChat(string groupName, User request)
        {
            await Clients.Group(groupName).SendAsync("AddUserToGroupChat", request);
        }
        public async Task Join(string groupName) 
        {
           await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task Leave(string groupName) 
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendMessageToGroup(string groupName, GroupChatMessage message) 
        {
            await Clients.Group(groupName).SendAsync("ReceivedGroupMessage", message);
        }

  

    }
}
