using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs
{
    public class ChatMessageDTO
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public string FromUserId { get; set; } = string.Empty;
        public int ToUserId { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public int UserId { get; set; }
    }
}
