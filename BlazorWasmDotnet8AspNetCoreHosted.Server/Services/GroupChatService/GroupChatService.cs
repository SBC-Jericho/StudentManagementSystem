using BlazorWasmDotnet8AspNetCoreHosted.Server.Data;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.RegularExpressions;

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

        public async Task<GroupChat> GetGroupById(int id)
        {

            var users = await _context.GroupChats
                     .Include(p => p.Paticipants)
                     .Where(p => p.Id == id)
                     .FirstOrDefaultAsync();

            return users;
        }

        public async Task<User?> GetSingleUser(int id)
        {
            var user = await _context.Users
                .Include(g => g.GroupChats)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (user is null)
                return null;

            return user;

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
        public async Task<User> AddUserToGroup(int userId, int groupId)
        {
            // Retrieve the group from the database

            var user = await _context.Users.FindAsync(userId);
            var groupChat = await _context.GroupChats
                .Include(g => g.Paticipants)
                .FirstOrDefaultAsync(g => g.Id == groupId);

            if (groupChat == null || user == null)
            {
                return null;
            }

            // Check if the user is already a participant in the group
            if (groupChat.Paticipants.Any(p => p.Id == userId))
            {
                return null;
            }

            // Add the user to the group
            groupChat.Paticipants.Add(user);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the added user
            return user;
        }

        public async Task<List<GroupChat>> AddGroupChat(GroupChatDTO request)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) 
            {
                return null;
            }
            var newGroup = new GroupChat()
            {
                Name = request.Name,
                Paticipants = new List<User>(),
                OwnerId = int.Parse(userId)
            };

            _context.Add(newGroup);
            await _context.SaveChangesAsync();


            if (!string.IsNullOrEmpty(userId))
            {
                // Add the creator to the group
                request.ParticipantIds.Add(int.Parse(userId));
            }

            // Add participants to the group
            foreach (var userIds in request.ParticipantIds)
            {
                var user = await _context.Users
                    .Include(u => u.GroupChats)
                    .FirstOrDefaultAsync(u => u.Id == userIds);

                if (user != null)
                {
                    user.GroupChats.Add(newGroup);
                }
            }

            await _context.SaveChangesAsync();

            return await _context.GroupChats.ToListAsync();
        }
        public async Task<List<GroupChatMessage>> GetGroupChatConversation(int groupChatId)
        {
            //Get the current logged-in user Id
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            List<GroupChatMessage> messages = await _context.GroupChatMessages
                .Where(m => (m.GroupChatId == groupChatId)
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

        public async Task<GroupChatMessage> SaveMessage(GroupChatMessage message)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var isMember = await _context.GroupChats
            .Where(g => g.Id == message.GroupChatId)
            .AnyAsync(g => g.Paticipants.Any(p => p.Id.ToString() == userId));

            if (!isMember) 
            {
                return null;
            }
            else
            {
                message.UserId = int.Parse(userId);
                message.Timestamp = DateTime.Now;
                message.User = await _context.Users.FindAsync(message.UserId);
                await _context.GroupChatMessages.AddAsync(message);
                await _context.SaveChangesAsync();

                return message;
            }
            

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

            var allUsers = await _context.Users.ToListAsync(); 

            var usersNotInGroup = allUsers.Except(groupMembers).ToList();

            return usersNotInGroup;
        }

       

        public async Task<List<User>> GetAllUsersExceptCurrent()
        {

            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Parse the user ID
            var user = await _context.Users.FindAsync(userId);

           
                var users = await _context.Users
                   .Include(p => p.Students)
                   .Include(p => p.Professor)
                    .Where(u => u.Id.ToString() != userId)
                   .ToListAsync();

            return users;
        }

        public async Task<string> GetSingleGroupName(int groupId)
        {
            
            var groupName = await _context.GroupChats
                     .Where(p => p.Id == groupId)
                      .Select(p => p.Name)
                     .FirstOrDefaultAsync();

            return groupName;
        }

        public async Task<List<GroupChat>?> UpdateGroupchat(int id, GroupChatNameDTO request) 
        {
            var groupchat = await _context.GroupChats
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
            if (groupchat is null)
                return null;

            groupchat.Name = request.Name;

            await _context.SaveChangesAsync();  

            return await _context.GroupChats.ToListAsync();
        }
        public Task<bool> AddParticipantsToGroup(GroupChat group, List<int> participantIds)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GroupChat>?> DeleteGroupChat(int id)
        {
            GroupChat? group = await _context.GroupChats.FindAsync(id);
            if (group is null)
                return null;

            _context.GroupChats.Remove(group);
            await _context.SaveChangesAsync();
            return await _context.GroupChats.ToListAsync();
        }

        public async Task<GetChatMembersDTO> GetGroupChatMembers(int groupChatId)
        {
            GroupChat? myGroupChat = await _context.GroupChats
                                    .Include(gc => gc.Paticipants)
                                    .FirstOrDefaultAsync(gc => gc.Id == groupChatId);

            if (myGroupChat != null && myGroupChat.Paticipants != null)
            {   
                GetChatMembersDTO response = new GetChatMembersDTO();
                response.OwnerId = myGroupChat.OwnerId;
                response.users = myGroupChat.Paticipants.Select(user => new User
                {
                    Id = user.Id,
                    Email = user.Email,
                    Avatar = user.Avatar,

                }).ToList();

                return response;
            }
            return new GetChatMembersDTO();
        }
    }
}
 