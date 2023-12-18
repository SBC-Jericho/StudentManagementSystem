using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Services.StudentService
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudent();
        Task<Student?> GetSingleStudent(int id);
        Task<List<Student>> AddStudent(studentDTO hero);
        Task<List<Student>?> UpdateStudent(int id, studentDTO request);
        Task<List<Student>?> DeleteStudent(int id);
    }
}
