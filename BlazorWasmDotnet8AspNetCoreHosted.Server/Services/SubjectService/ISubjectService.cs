using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Services.SubjectService
{
    public interface ISubjectService
    {
        Task<List<Subject>> GetAllSubject();
        Task<Subject?> GetSingleSubject(int id);
        Task<List<Subject>> AddSubject(SubjectDTO subject);
        Task<List<Subject>?> UpdateSubject(int id, SubjectDTO request);
        Task<List<Subject>?> DeleteSubject(int id);
    }
}
