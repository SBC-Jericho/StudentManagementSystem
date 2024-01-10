using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Services.ChatMessageService
{
    public interface IChatMessageService
    {
        Task<List<ChatMessage>> GetAllChatMessage();
        Task<ChatMessage?> GetSingleChatMessage(int id);
        Task<List<ChatMessage>> AddChatMessage(ChatMessageDTO request);
        Task<List<ChatMessage>?> UpdateChatMessage(int id, ChatMessageDTO request);
        Task<List<ChatMessage>?> DeleteAnnouncement(int id);

    }
}
