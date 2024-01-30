using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using System.Runtime.InteropServices;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Hubs
{
    public interface IChatClient
    {
        Task ReceivedMessage(ChatMessage message, string userName);
        Task ReceiveChatNotification(string message, int receiverUserId, string senderUserId);
        Task ReceiveGroupChatNotification(string message, int receiverUserId, int groupChatId);
        Task ReceiveNewGroupChat(GroupChat groupChat);
        Task DeleteGroupChatHub(int groupId);
        Task UpdateGroupName(int groupId, GroupChatNameDTO request);
        Task RemoveUser(int userId);
        Task AddUserToGroupChat(User request);
        Task ReceivedGroupMessage(GroupChatMessage message);

    }
}
