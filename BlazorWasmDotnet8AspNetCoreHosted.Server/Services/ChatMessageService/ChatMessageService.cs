using BlazorWasmDotnet8AspNetCoreHosted.Server.Services.UserService;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using System.Security.Claims;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Services.ChatMessageService
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        public ChatMessageService(DataContext context, IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }
        public async Task<List<ChatMessage>> AddChatMessage(ChatMessageDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<List<ChatMessage>?> DeleteAnnouncement(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ChatMessage>> GetAllChatMessage()
        {
            throw new NotImplementedException();
        }

        public Task<ChatMessage?> GetSingleChatMessage(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ChatMessage>?> UpdateChatMessage(int id, ChatMessageDTO request)
        {
            throw new NotImplementedException();
        }
    }
}
