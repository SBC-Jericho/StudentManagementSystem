using BlazorWasmDotnet8AspNetCoreHosted.Server.Data;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Services.GroupChatService
{
    public class GroupChatService : IGroupChatService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GroupChatService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<GroupChat>> GetAllGroup()
        {
            var group = await _context.GroupChats
                .ToListAsync();

            return group;
        }
        public async Task<List<GroupChat>> GetAllGroupsForUser()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _context.Users
                .Include(u => u.GroupChats)
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);

            if (user == null)
            {
                // Handle the case where the user is not found
                return new List<GroupChat>();
            }

            var groupsForUser = user.GroupChats;

            return groupsForUser;
        }



        public async Task<GroupChat?> GetSingleGroup(int groupId)
        {
            var result = await _context.GroupChats.FindAsync(groupId);

            if (result == null)
            {
                return null;
            }
            return result;
        }
        public async Task<bool> AddUserToGroup(int userId, int groupChatId)
        {
            
                var user = await _context.Users.FindAsync(userId);
                var groupChat = await _context.GroupChats.FindAsync(groupChatId);
                if (user == null && groupChat == null && groupChat.Paticipants.Any(m => m.Id == userId))
                {
                    // User or group not found
                    return false;
                }
                // Add the user to the group
                groupChat.Paticipants.Add(user);
                // Save changes to the database
                await _context.SaveChangesAsync();
                return true;
            

        }

        public async Task<bool> RemoveUserToGroup(int userId, int groupChatId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                var groupChat = await _context.GroupChats
                    .Include(g => g.Paticipants)
                    .FirstOrDefaultAsync(g => g.Id == groupChatId);

                if (user == null || groupChat == null)
                {
                    return false;
                }

                var memberToRemove = groupChat.Paticipants.FirstOrDefault(m => m.Id == userId);

                if (memberToRemove == null)
                {
                    return false;
                }

                groupChat.Paticipants.Remove(memberToRemove);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
     
        public async Task<List<GroupChat>> AddGroupChat(GroupChatDTO request)
        {
            var newGroup = new GroupChat() 
            {
                Name = request.Name, 
            };

            _context.Add(newGroup);
            await _context.SaveChangesAsync();

            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _context.Users
                .Include(u => u.GroupChats)
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);

            if (user != null)
            {
                // Add the user to the group
                user.GroupChats.Add(newGroup);
                await _context.SaveChangesAsync();
            }
            return await _context.GroupChats.ToListAsync(); 
        }
        public async Task<List<GroupChatMessage>> GetGroupChatConversation(int groupChatId)
        {
            // Get the current logged-in user Id
            //var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            List<GroupChatMessage> messages = await _context.GroupChatMessages
                .Where(m =>
                    (m.GroupChatId == groupChatId)
                     // Assuming User is a navigation property in GroupChatMessage
                )
                .OrderBy(message => message.Timestamp)
                .Include(message => message.User) // Include User information
                .Select(x => new GroupChatMessage 
                {
                    Id = x.Id,
                    Timestamp = x.Timestamp,
                    Content = x.Content,
                    User = x.User,
                    UserId = x.UserId,
                    GroupChat = x.GroupChat,
                    GroupChatId = x.GroupChatId
                }).ToListAsync();

            return messages;
        }

        public async Task SaveMessage(GroupChatMessage message)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            message.UserId = int.Parse(userId);
            message.Timestamp = DateTime.Now;
            message.User = await _context.Users.FindAsync(message.UserId);

            await _context.GroupChatMessages.AddAsync(message);
            await _context.SaveChangesAsync();

        }

        public async Task<List<User>> GetGroupMembers(int groupId)
        {
            var groupChat = await _context.GroupChats
                .Where(group => group.Id == groupId)
                .SelectMany(group => group.Paticipants)
                .ToListAsync();

            return groupChat;
        }

        public async Task<List<User>> GetNotMembers(int groupId)
        {
            var groupMembers = await GetGroupMembers(groupId);

            var allUsers = await _context.Users.ToListAsync(); // Replace with your actual user retrieval logic

            var usersNotInGroup = allUsers.Except(groupMembers).ToList();

            return usersNotInGroup;
        }

        public async Task<List<GroupChat>> DeleteGroup(int id)
        {
            var groupChat = await _context.GroupChats.FindAsync(id);
            if (groupChat == null) 
            {
                return null;
            }

            _context.GroupChats.Remove(groupChat);
            await _context.SaveChangesAsync();
            return await _context.GroupChats.ToListAsync();
        }

        public async Task<string> GetSingleGroupName(int groupId)
        {
            
            var groupName = await _context.GroupChats
                     .Where(p => p.Id == groupId)
                      .Select(p => p.Name)
                     .FirstOrDefaultAsync();

            return groupName;
        }
    }
}
