using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs
{
    public class MessageCountDTO
    {
        public int Count { get; set; }
        public List<ChatMessage> chatMessages { get; set; }
    }
}
