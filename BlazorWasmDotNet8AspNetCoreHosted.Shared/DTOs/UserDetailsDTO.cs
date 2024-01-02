using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs
{
    public class UserDetailsDTO
    {
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; } = DateTime.Now;
        public string? Image { get; set; } = string.Empty;
        public string? Contact { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
    }
}
