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
        public async Task<List<User>> GetAllUsers()
        {
            //Get the user ID of current logged in user
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //return all users except the current user
            //to avoid seeing your name in your own contact list
            //this is for contact list

            //var users = await _context.Users
            //   .Where(user => user.Id.ToString() != userId)
            //   .ToListAsync();

            //return users;
         var usersWithLastMessage = await _context.Users
        .Where(user => user.Id.ToString() != userId)
        .Select(user => new
        {
            User = user,
            LastMessage = _context.ChatMessages.Where(message => (message.FromUserId == user.Id && message.ToUserId.ToString() == userId) ||(message.FromUserId.ToString() == userId && message.ToUserId == user.Id))
                .OrderByDescending(message => message.CreatedDate)
                .FirstOrDefault()
        }).ToListAsync();

            var orderedUsers = usersWithLastMessage
                .OrderByDescending(entry => entry.LastMessage?.CreatedDate ?? DateTime.MinValue)
                .Select(entry => entry.User)
                .ToList();

            return orderedUsers;

        }

        public async Task<List<ChatMessage>> GetConversation(int receiverId)
        {
            // get the current logged in user Id
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

        public async Task<int> MessageCountFromAllUser(int receiverId)
        {
            // get the current logged in user Id
            //var senderId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            List<ChatMessage> messages = await _context.ChatMessages
           .Where(h => h.FromUserId != receiverId)
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

            return messages.Count;
        }
        public async Task<int> MessageCountFromOneUser(int otherUserId)
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // Retrieve the count of messages from other user to current user
            int messageCount = await _context.ChatMessages
                .Where(h => h.FromUserId == otherUserId && h.ToUserId.ToString() == currentUserId)
                .CountAsync();

            // Find the current user
            User? user = await _context.Users.FindAsync(int.Parse(currentUserId));

            // Update the message count property of the current user
            if (user != null)
            {
                // Update the message count property of the current user
                user.MessageCount = messageCount;

                // Save changes to the database
                await _context.SaveChangesAsync();
            }
            else
            {
                // Handle the case where the user is not found (optional)
                // You may want to throw an exception, log an error, or handle it in another way
                // For now, let's log an error
                Console.WriteLine($"User with ID {currentUserId} not found.");
            }

            // Return the message count
            return messageCount;
        }


        //public async Task<(int messagesSentBySender, int messagesSentToReceiver)> GetConversationMessageCounts(int receiverId)
        //{
        //    // Get the current logged-in user Id
        //    var senderId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //    // Count messages sent by sender to receiver
        //    int messagesSentBySender = await _context.ChatMessages
        //        .Where(h => h.FromUserId == senderId && h.ToUserId == receiverId)
        //        .CountAsync();

        //    // Count messages sent by receiver to sender
        //    int messagesSentToReceiver = await _context.ChatMessages
        //        .Where(h => h.FromUserId == receiverId && h.ToUserId == senderId)
        //        .CountAsync();

        //    return (messagesSentBySender, messagesSentToReceiver);
        //}

        //public async Task<int> MessageCount(int receiverId)
        //{
        //    // get the current logged in user Id
        //    //var senderId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //    List<ChatMessage> messages = await _context.ChatMessages
        //   .Where(h => (h.FromUserId == receiverId && h.ToUserId == senderId)
        //   || (h.FromUserId == senderId && h.ToUserId == receiverId))
        //   .OrderBy(a => a.CreatedDate)
        //   .Include(a => a.Users)
        //   .Select(x => new ChatMessage
        //   {
        //       FromUserId = x.FromUserId,
        //       ToUserId = x.ToUserId,
        //       Message = x.Message,
        //       CreatedDate = x.CreatedDate,
        //       Id = x.Id,
        //       Users = x.Users
        //   }).ToListAsync();

        //    return messages.Count;
        //}
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

        public Task<int> MessageCount(int senderId, int receiverId)
        {
            throw new NotImplementedException();
        }

        public Task<int> MessageCountFromOneUser(int currentUser, int otherUser)
        {
            throw new NotImplementedException();
        }
    }
}
