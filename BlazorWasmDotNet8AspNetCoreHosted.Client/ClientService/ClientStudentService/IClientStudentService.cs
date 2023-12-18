using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;

namespace BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientStudentService
{
    public interface IClientStudentService
    {
        List<Student> ClientStudent { get; set; }
        Task<List<Student>> GetAllStudent();
        Task<Student?> GetSingleStudent(int id);
        Task AddStudent(Student hero);
        Task UpdateStudent(int id, Student request);
        Task DeleteStudent(int id);
    }
}
