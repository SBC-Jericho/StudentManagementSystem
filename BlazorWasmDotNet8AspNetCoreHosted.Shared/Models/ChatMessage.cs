using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorWasmDotNet8AspNetCoreHosted.Shared.Models
{
    public class ChatMessage 
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public  int FromUserId { get; set; } 
        public int ToUserId { get; set; } 
        public DateTime? CreatedDate { get; set; }
        public User? Users {  get; set; } 
        public int UserId { get; set; }
    }
}
