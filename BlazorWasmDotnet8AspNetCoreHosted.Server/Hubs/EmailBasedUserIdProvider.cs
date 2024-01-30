using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Hubs
{
    public class EmailBasedUserIdProvider : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(ClaimTypes.Name)?.Value!;
        }
    }
}
