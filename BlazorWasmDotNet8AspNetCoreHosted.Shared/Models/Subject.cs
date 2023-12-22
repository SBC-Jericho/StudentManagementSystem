using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorWasmDotNet8AspNetCoreHosted.Shared.Models
{
    public class Subject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please, give this subject a name: ")]
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        public List<EnrolledSubjects> EnrolledSubjects { get; set; }
    }
}
