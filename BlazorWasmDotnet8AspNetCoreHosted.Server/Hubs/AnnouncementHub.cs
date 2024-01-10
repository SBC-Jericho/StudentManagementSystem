using Microsoft.AspNetCore.SignalR;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Hubs
{
    public class AnnouncementHub : Hub 
    {
        public async Task SendAnnouncement(string message)
        {
            await Clients.All.SendAsync("ReceivedAnnouncement", message);
        }
    }
}
    