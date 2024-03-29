﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs
{
    public class UserDetailsDTO
    {
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; } = DateTime.Now;
        public string? Image { get; set; } = string.Empty;
        //[Required(ErrorMessage = "Contact is required.")]
        //[Phone (ErrorMessage = "Invalid contact format.")]
        //[RegularExpression("^(09|\\+639)\\d{9}$", ErrorMessage = "The number should start with +639 or 09 followed by 9 numbers.")]
        public string? Contact { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
    }
}
