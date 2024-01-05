using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs
{
    public class SubjectDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public List<int> ProfessorIds { get; set; }
    }
}
