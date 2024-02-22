using BlazorWasmDotnet8AspNetCoreHosted.Server.Services.ChatMessageService;
using BlazorWasmDotnet8AspNetCoreHosted.Server.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessageController : ControllerBase
    {
        private readonly IChatMessageService _chatMessageService;

        public ChatMessageController(IChatMessageService chatMessageService)
        {
            _chatMessageService = chatMessageService;
        }
        //[HttpGet("get-chat/{receiverId}")]
        //public async Task<ActionResult<List<ChatMessage>>> GetConversationAsync(int receiverId)
        //{
        //    List<ChatMessage> result = await _chatService.GetConversationAsync(receiverId);
        //    if (result is null)
        //    {
        //        return BadRequest("No Conversation Found");
        //    }
        //    return Ok(result);
        //}

        [HttpGet("get-conversation/{receiverId}")]

        public async Task<ActionResult<List<ChatMessage>>> GetConversation(int receiverId)
        {
            List<ChatMessage> result = await _chatMessageService.GetConversation(receiverId);
            if (result is null)
            {
                return BadRequest("No Conversation Found");
            }
            return Ok(result);
        }

        [HttpGet("users")]
        public async Task<ActionResult<List<User>>> GetAllUser()
        {
            List<User> result = await _chatMessageService.GetAllUsers();
            if (result is null)
            {
                return BadRequest("No User Found");
            }
            return Ok(result);

        }

        [HttpGet("single-user/{id}")]
        public async Task<ActionResult<User>> GetSingleUser(int id)
        {
            var result = await _chatMessageService.GetSingleUser(id);
            if (result == null)
            {
                return NotFound("User Not Found");
            }
            return Ok(result);
        }

        [HttpGet("get-message-count")]
        public async Task<ActionResult<int>> MessageCount(int senderId, int receiverId) 
        {
            var result = await _chatMessageService.MessageCount(senderId, receiverId);
            return Ok(result);
        }

        [HttpGet("get-message-count-from-others")]
        public async Task<ActionResult<int>> MessageCountFromOneUser(int receiverId)
        {
            var result = await _chatMessageService.MessageCountFromOneUser(receiverId);
            return Ok(result);
        }
        [HttpGet("get-message-count-from-all")]
        public async Task<ActionResult<int>> MessageCountFromAllUser(int receiverId)
        {
            var result = await _chatMessageService.MessageCountFromAllUser(receiverId);
            return Ok(result);
        }



        //[HttpGet("user-detail/{id}")]
        //public async Task<ActionResult<User>> GetUserDetailsAsync(int id)
        //{
        //    var result = await _chatService.GetUserDetailsAsync(id);
        //    if (result is null)
        //        return NotFound("User not found.");
        //    return Ok(result);
        //}

        [HttpPost]
        public async Task SaveMessage(ChatMessage request) 
        {
             await _chatMessageService.SaveMessage(request);
         
        }


    }
}
