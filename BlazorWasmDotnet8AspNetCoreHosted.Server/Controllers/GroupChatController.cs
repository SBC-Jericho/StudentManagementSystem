using BlazorWasmDotnet8AspNetCoreHosted.Server.Services.GroupChatService;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupChatController : ControllerBase
    {
        private readonly IGroupChatService _groupChatService;

        public GroupChatController(IGroupChatService groupChatService)
        {
            _groupChatService = groupChatService;
        }

        [HttpGet("get-all-group")]
        public async Task<ActionResult<List<User>>> GetAllGroup() 
        {
            var result = await _groupChatService.GetAllGroup();
            return Ok(result);
        }

        [HttpGet("get-group-members/{groupId}")]
        public async Task<ActionResult<List<User>>> GetGroupMembers(int groupId) 
        {
            var result = await _groupChatService.GetGroupMembers(groupId);
            if (result != null) 
            {
                return Ok(result);
            }
            return NotFound("No members found");
        }

        [HttpGet("get-notgroup-members/{groupId}")]
        public async Task<ActionResult<List<User>>> GetNotMembers(int groupId)
        {
            var result = await _groupChatService.GetNotMembers(groupId);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("No members found");
        }

        [HttpGet("get-group-single-user/")]
        public async Task<ActionResult<List<GroupChat>>> GetAllGroupsForUser()
        {
            var result = await _groupChatService.GetAllGroupsForUser();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("No members found");
        }

        [HttpGet("get-groupchat-conversation/{groupId}")]

        public async Task<ActionResult<List<GroupChatMessage>>> GetConversation(int groupId)
        {
            List<GroupChatMessage> result = await _groupChatService.GetGroupChatConversation(groupId);
            if (result is null)
            {
                return BadRequest("No Conversation Found");
            }
            return Ok(result);
        }

        [HttpGet("single-group/{id}")]
        public async Task<ActionResult<User>> GetSingleGroup(int id)
        {
            var result = await _groupChatService.GetSingleGroup(id);
            if (result == null)
            {
                return NotFound("Group Not Found");
            }
            return Ok(result);
        }

        [HttpGet("single-group-name/{id}")]
        public async Task<ActionResult<string>> GetSingleGroupName(int id)
        {
            var result = await _groupChatService.GetSingleGroupName(id);

            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult<List<GroupChat>>> AddGroupChat(GroupChatDTO request)
        {
            var result = await _groupChatService.AddGroupChat(request);
            return Ok(result);
        }


        [HttpPost("add-user-to-group/{userId}/{groupId}")]
        public async Task<ActionResult<bool>> AddUserToGroup(int userId, int groupId)
        {
            var result = await _groupChatService.AddUserToGroup(userId, groupId);
            return Ok(result);
        }

       

        [HttpDelete("remove-user-to-group/{userId}/{groupId}")]
        public async Task<ActionResult<bool>> RemoveUserToGroup(int userId, int groupId)
        {
            var result = await _groupChatService.RemoveUserToGroup(userId, groupId);
            return Ok(true);
        }

        [HttpDelete("delete-group{id}")]
        public async Task<ActionResult<List<GroupChat>>> DeleteGroup(int id) 
        {
            var result = await _groupChatService.DeleteGroup(id);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPost("save-group-message")]
        public async Task SaveMessage(GroupChatMessage request)
        {
            await _groupChatService.SaveMessage(request);

        }
    }
}
