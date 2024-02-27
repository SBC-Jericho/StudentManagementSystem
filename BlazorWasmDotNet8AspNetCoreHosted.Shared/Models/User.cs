using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorWasmDotNet8AspNetCoreHosted.Shared.Models
{
    public class User
    {
        public int Id { get; set; }  
        public string Email { get; set; } = string.Empty;
        public string? Avatar { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; }
        public Student? Students { get; set; }
        public Professor? Professor { get; set;}

        [JsonIgnore]
        public List<ChatMessage> ChatMessages{ get; set; } = new List<ChatMessage>();
        [JsonIgnore]
        public List<GroupChat> GroupChats { get; set; } = new List<GroupChat>();
        public bool ActiveStatus { get; set; } = false;
        public int MessageCount { get; set; } = 0;
    }
}
