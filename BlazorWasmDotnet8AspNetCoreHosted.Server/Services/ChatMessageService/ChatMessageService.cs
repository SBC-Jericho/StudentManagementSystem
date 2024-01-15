using BlazorWasmDotnet8AspNetCoreHosted.Server.Services.UserService;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Services.ChatMessageService
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChatMessageService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        //private async Task<List<ChatMessage>> GetConvo(int contactId)
        //{
        //    var userId = _httpContextAccessor?.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //    List<ChatMessage> messages = await _context.ChatMessages
        //   .Where(h => (h.FromUserId == contactId.ToString() && h.ToUserId.ToString() == userId) || (h.FromUserId.ToString() == userId && h.ToUserId == contactId.ToString()))
        //   .OrderBy(a => a.CreatedDate)
        //   .Include(a => a.Users)
        //   .Select(x => new ChatMessage
        //   {
        //       FromUserId = x.FromUserId,
        //       Message = x.Message,
        //       CreatedDate = x.CreatedDate,
        //       Id = x.Id,
        //       ToUserId = x.ToUserId,
        //       Users = x.Users
        //   }).ToListAsync();

        //    return messages;
        //}
        public async Task<List<User>> GetAllUsers()
        {
            //Get the user ID of current logged in user
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //return all users except the current user
            //to avoid seeing your name in your own contact list
            //this is for contact list

            var users = await _context.Users
               .Where(user => user.Id.ToString() != userId)
               .ToListAsync();

            return users;

        }

        public async Task<List<ChatMessage>> GetConversation(int receiverId)
        {
            var senderId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            List<ChatMessage> messages = await _context.ChatMessages
           .Where(h => (h.FromUserId == receiverId && h.ToUserId.ToString() == senderId) 
           || (h.FromUserId.ToString() == senderId && h.ToUserId == receiverId))
           .OrderBy(a => a.CreatedDate)
           .Include(a => a.Users)
           .Select(x => new ChatMessage
           {
               FromUserId = x.FromUserId,
               ToUserId = x.ToUserId,
               Message = x.Message,
               CreatedDate = x.CreatedDate,
               Id = x.Id,
               Users = x.Users
           }).ToListAsync(); 

            return messages;
        }

        public async Task<User> GetSingleUser(int userId)
        {
            var user = await _context.Users
                .Where(p => p.Id == userId)
                .FirstOrDefaultAsync();
            if (user == null)
                return null;
            return user;
        }
        //public async Task<User> GetUserDetailsAsync(int userId)
        //{
        //    User? user = await _context.Users
        //             .Where(p => p.Id == userId)
        //             .FirstOrDefaultAsync();
        //    if (user == null)
        //        return null;

        //    return user;
        //}

        public async Task SaveMessage(ChatMessage message) 
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            message.FromUserId = int.Parse(userId);
            message.CreatedDate = DateTime.Now;
            message.Users = await _context.Users.FindAsync(message.FromUserId);

            await _context.ChatMessages.AddAsync(message);
            await _context.SaveChangesAsync();

        }
      
    }
}
