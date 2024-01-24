using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs
{
    public class AddUserToGroupDTO
    {
        public int UserId { get; set; } 
        public int GroupId { get; set; }    
    }
}
