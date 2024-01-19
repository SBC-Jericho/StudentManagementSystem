using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWasmDotNet8AspNetCoreHosted.Shared.Models
{
    public class GroupChatMessage
    {
        public int Id { get; set; }
        public DateTime? Timestamp { get; set; } = DateTime.Now;
        public string Content { get; set; } = string.Empty;
        public User? User { get; set; }
        public int UserId { get; set; }
        public GroupChat? GroupChat { get; set; }
        public int GroupChatId { get; set; }
    }
}
