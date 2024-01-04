using BlazorWasmDotnet8AspNetCoreHosted.Server.Services.EnrolledSubjectsService;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrolledSubjectController : ControllerBase
    {
        private readonly IEnrolledSubjectsService _enrolledSubjectsService;
        public EnrolledSubjectController(IEnrolledSubjectsService enrolledSubjectsService)
        {
            _enrolledSubjectsService = enrolledSubjectsService;
        }
        [HttpGet]
        public async Task<ActionResult<List<EnrolledSubjects>>> GetAllEnrolledSubject()
        {

            return await _enrolledSubjectsService.GetAllEnrolledSubject();
        }

        [HttpGet("enrollment-details-student/{id}")]
        public async Task<List<EnrolledSubjects>> GetSingleEnrolledSubjects(int id) 
        {
            var result = await _enrolledSubjectsService.GetSingleEnrolledSubjects(id);
            return result;
        
        }
        
        [HttpGet("student-enrolled/{id}")]
        public async Task<List<EnrolledSubjects>> GetSingleEnrolledStudent(int id) 
        {
            var result = await _enrolledSubjectsService.GetSingleEnrolledStudent(id);
            return result;
        
        }


        [HttpPost("add-enrolled-sub")]
        public async Task<ActionResult<int>> AddEnrolledSubjects(EnrollmentDTO request)
        {
            var result = await _enrolledSubjectsService.AddEnrolledSubject(request);
            if (result == 0) 
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
