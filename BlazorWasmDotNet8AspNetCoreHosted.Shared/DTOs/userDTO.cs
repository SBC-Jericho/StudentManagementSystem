using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs
{
    public class userDTO
    {
        [Required]  
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [RegularExpression(@"^[a-zA-Z0-9]{1,}[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; }
        [Required]
        public string? Avatar { get; set; } = string.Empty;
        public UserDetailsDTO? userDetailsDTO { get; set; } = new UserDetailsDTO();
    }
}
