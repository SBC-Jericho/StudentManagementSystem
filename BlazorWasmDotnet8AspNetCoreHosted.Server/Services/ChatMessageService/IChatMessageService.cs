using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Services.ChatMessageService
{
    public interface IChatMessageService
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetSingleUser(int userId);
        Task SaveMessage(ChatMessage message);
        Task <List<ChatMessage>> GetConversation(int receiverId);
        Task<int> MessageCount(int senderId, int receiverId);
        Task<int> MessageCountFromOneUser(int otherUser);
        Task<int> MessageCountFromAllUser(int receiverId);



    }
}
