using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;

namespace BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientChatMessageService
{
    public interface IClientChatMessageService
    {
        Task <List<User>>GetAllUsers();
        Task SaveMessage(ChatMessage message);
        Task<List<ChatMessage>> GetConversation(int receiverId);
        Task<User?> GetSingleUser(int userId);
    }
}
