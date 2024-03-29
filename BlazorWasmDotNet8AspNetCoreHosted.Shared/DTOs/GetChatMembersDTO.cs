﻿using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs
{
    public class GetChatMembersDTO
    {
        public int OwnerId { get; set; }
        public List<User> users { get; set; } = new List<User>();
    }
}
