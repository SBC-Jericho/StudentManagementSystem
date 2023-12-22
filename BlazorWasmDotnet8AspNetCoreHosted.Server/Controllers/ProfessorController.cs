using BlazorWasmDotnet8AspNetCoreHosted.Server.Services.ProfessorService;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly IProfessorService _professorService;
        public ProfessorController(IProfessorService professorService)
        {
            _professorService = professorService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Professor>>> GetAllProfessor()
        {

            return await _professorService.GetAllProfessor();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Professor>> GetSingleProfessor(int id)
        {
            var result = await _professorService.GetSingleProfessor(id);
            if (result is null)
                return NotFound("Hero Not Found");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Professor>>> AddProfessor(ProfessorDTO prof)
        {
            var result = await _professorService.AddProfessor(prof);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Professor>> UpdateProfessor(int id, ProfessorDTO request)
        {
            var result = await _professorService.UpdateProfessor(id, request);
            if (result is null)
                return NotFound("Hero Not Found");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Professor>> DeleteProfessor(int id)
        {
            var result = await _professorService.DeleteProfessor(id);
            if (result is null)
                return NotFound("Hero Not Found");

            return Ok(result);
        }
    }
}
