using BlazorWasmDotnet8AspNetCoreHosted.Server.Services.SubjectService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using static MudBlazor.Colors;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Subject>>> GetAllSubject()
        {

            return await _subjectService.GetAllSubject();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Subject>> GetSingleSubject(int id)
        {
            var result = await _subjectService.GetSingleSubject(id);
            if (result is null)
                return NotFound("Subject Not Found");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Subject>>> AddSubject(SubjectDTO subject)
        {
            var result = await _subjectService.AddSubject(subject);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Subject>> UpdateSubject(int id, SubjectDTO request)
        {
            var result = await _subjectService.UpdateSubject(id, request);
            if (result is null)
                return NotFound("Subject Not Found");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Subject>> DeleteSubject(int id)
        {
            var result = await _subjectService.DeleteSubject(id);
            if (result is null)
                return NotFound("Subject Not Found");

            return Ok(result);
        }
    }
}
