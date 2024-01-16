using Microsoft.AspNetCore.SignalR;

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

        public  Task AddGroup(string group) 
        {
             return Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public Task RemoveGroup(string groupName) 
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendMessageToGroup(string groupName, string message) 
        {
            await Clients.Group(groupName).SendAsync("ReceivedMessage", message);
        }
    }
}
