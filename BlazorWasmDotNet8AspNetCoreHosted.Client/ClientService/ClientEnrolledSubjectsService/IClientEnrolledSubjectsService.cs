using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;

namespace BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientEnrolledSubjectsService
{
    public interface IClientEnrolledSubjectsService
    {
        List<EnrolledSubjects> ClientEnrolledSubjects { get; set; }

        Task<int> AddEnrolledSubject(EnrollmentDTO request);
        Task<List<EnrolledSubjects>> GetAllEnrolledSubject();
        Task<List<EnrolledSubjects>> GetSingleEnrolledSubjects(int id);
    }
}
