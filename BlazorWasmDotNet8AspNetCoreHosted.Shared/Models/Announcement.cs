using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWasmDotNet8AspNetCoreHosted.Shared.Models
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty; 
        public DateTime? DateCreated { get; set; } = DateTime.Now;
    }
}
