using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Services.GroupChatService
{
    public interface IGroupChatService
    {
        Task<List<GroupChat>> AddGroupChat(GroupChatDTO request);
        Task<User> AddUserToGroup(int userId, int groupChatId);
        Task<bool> RemoveUserToGroup(int userId, int groupChatId);
        Task<List<User>> GetGroupMembers(int groupId);
        Task<List<User>> GetNotMembers(int groupId);
        Task<User?> GetSingleUser(int id);
        Task<List<GroupChat>> GetAllGroupsForUser();
        Task<List<GroupChatMessage>> GetGroupChatConversation(int receiverId);
        Task<List<GroupChat>> GetAllGroup();
        Task<GroupChat?> GetSingleGroup(int Id);
        Task <GroupChatMessage>SaveMessage(GroupChatMessage message);
        Task<string> GetSingleGroupName(int groupId);
        Task<List<User>> GetAllUsersExceptCurrent();
        Task<List<GroupChat>?> UpdateGroupchat(int id, GroupChatNameDTO request);
        Task<GroupChat> GetGroupById(int id);
        Task<List<GroupChat>?> DeleteGroupChat(int id);
        Task<GetChatMembersDTO> GetGroupChatMembers(int groupChatId);
    }
}
