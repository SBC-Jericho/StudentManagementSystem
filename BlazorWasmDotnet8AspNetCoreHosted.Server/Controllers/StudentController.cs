using BlazorWasmDotnet8AspNetCoreHosted.Server.Services.StudentService;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService StudentService)
        {
            _studentService = StudentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetAllStudent()
        {

            return await _studentService.GetAllStudent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetSingleStudent(int id)
        {
            var result = await _studentService.GetSingleStudent(id);
            if (result is null)
                return NotFound("Hero Not Found");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Student>>> AddStudent(studentDTO hero)
        {
            var result = await _studentService.AddStudent(hero);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> UpdateStudent(int id, studentDTO request)
        {
            var result = await _studentService.UpdateStudent(id, request);
            if (result is null)
                return NotFound("Hero Not Found");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var result = await _studentService.DeleteStudent(id);
            if (result is null)
                return NotFound("Hero Not Found");

            return Ok(result);
        }
    }
}
