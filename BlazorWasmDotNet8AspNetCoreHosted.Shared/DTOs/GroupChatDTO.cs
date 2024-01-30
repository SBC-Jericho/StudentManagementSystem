﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs
{
    public class GroupChatDTO
    {
        public string Name { get; set; } = string.Empty;
        public List<int> ParticipantIds { get; set; } = new List<int>();
        

    }
}
