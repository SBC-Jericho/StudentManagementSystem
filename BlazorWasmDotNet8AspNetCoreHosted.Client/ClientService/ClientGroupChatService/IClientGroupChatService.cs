using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
using System.Threading.Tasks;

namespace BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientGroupChatService
{
    public interface IClientGroupChatService
    {
        List<GroupChat> clientGroupChat { get; set; }
        List<User> clientUser { get; set; }
        Task AddGroupChat(GroupChatDTO request);
        Task DeleteGroup(int id);
        Task<GroupChat> AddUserToGroup(AddUserToGroupDTO request);
        Task RemoveUserToGroup(int userId, int groupChatId);
        Task<List<User>>GetGroupMembers(int groupId);
        Task<List<User>>GetNotMembers(int groupId);
        Task<List<GroupChatMessage>> GetGroupChatConversation(int receiverId);
        Task<List<GroupChat>> GetAllGroupsForUser();
        Task <List<GroupChat>> GetAllGroup();
        Task<List<User>> GetAllUsersExceptCurrent();
        Task<GroupChat?> GetSingleGroup(int Id);
        Task SaveMessage(GroupChatMessage message);
        Task<string> GetSingleGroupName(int id);

        Task<User?> GetSingleUser(int id);
    }
}
