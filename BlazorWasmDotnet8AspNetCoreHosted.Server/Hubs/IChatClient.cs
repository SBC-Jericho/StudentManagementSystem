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
        Task RemoveUser(string userEmail);
        Task AddUserToGroupChat(User request);
        Task ReceivedGroupMessage(GroupChatMessage message, string groupName);
        Task ReceiveSystemMessage(string m);
        Task onError(string error);
        Task UpdateUserStatus(string email, bool status);
        Task UpdateStatusToOff(string status);
        Task CountOtherUserMessage(int senderId);

        Task RemoveMemberNotification(GroupChatMessage message, string userName);
    }
}
