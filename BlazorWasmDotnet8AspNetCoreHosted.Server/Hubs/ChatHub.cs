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
        public async Task Join(string groupName) 
        {
           await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task Leave(string groupName) 
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendMessageToGroup(GroupChatMessage message, string groupName) 
        {
            await Clients.Group(groupName).SendAsync("ReceivedMessage", message, groupName);
        }

  

    }
}
