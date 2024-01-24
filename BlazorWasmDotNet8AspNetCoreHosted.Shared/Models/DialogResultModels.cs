using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWasmDotNet8AspNetCoreHosted.Shared.Models
{
    public class DialogResultModels
    {
        public string? GroupName { get; set; }
        public List<User> SelectedUserIds { get; set; } = new List<User>();
    }
}
