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
            
        public async Task CreateGroup(GroupChat groupChat) 
        {
            await Clients.All.SendAsync("ReceiveNewGroupChat", groupChat);
        }                                                                                                                  
        public async Task RemoveUserToGroup(string groupName,  int userId) 
        {
            await Clients.Group(groupName).SendAsync("RemoveUser", userId);
        }

        public async Task AddToGroup(string groupName, User request)
        {
            await Clients.Group(groupName).SendAsync("ReceiveUserToAdd", request);
        }
        public async Task AddUser(string groupName, List<int> user)
        {
            await Clients.Group(groupName).SendAsync("AddUserToGroup", user);
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
