using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorWasmDotNet8AspNetCoreHosted.Shared.Models
{
    public class GroupChat
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<User>? Paticipants { get; set; } = new List<User>();
        public int OwnerId { get; set; }
        public List<GroupChatMessage> Messages { get; set; } = new List<GroupChatMessage>();

    }
}
