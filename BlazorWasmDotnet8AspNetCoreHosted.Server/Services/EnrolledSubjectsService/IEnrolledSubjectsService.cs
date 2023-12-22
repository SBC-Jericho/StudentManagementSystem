using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Services.EnrolledSubjectsService
{
    public interface IEnrolledSubjectsService
    {
      
        Task<int> AddEnrolledSubject(EnrollmentDTO request);

        Task<List<EnrolledSubjects>> GetAllEnrolledSubject();

        Task<List<EnrolledSubjects>> GetSingleEnrolledSubjects(int id);
    }
}
