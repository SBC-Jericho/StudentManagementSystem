using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs
{
    public class AnnouncementDTO
    {
        public string Message { get; set; } = string.Empty;
        public DateTime? DateCreated { get; set; } = DateTime.Now;
    }
}
