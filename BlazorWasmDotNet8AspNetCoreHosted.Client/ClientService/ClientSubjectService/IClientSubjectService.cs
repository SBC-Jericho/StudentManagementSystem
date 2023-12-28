using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;

namespace BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientSubjectService
{
    public interface IClientSubjectService
    {
        List<Subject> ClientSubject { get; set; }
        Task<List<Subject>> GetAllSubject();
        Task<Subject?> GetSingleSubject(int id);
        Task AddSubject(SubjectDTO subject);
        Task UpdateSubject(int id, Subject request);
        Task DeleteSubject(int id);

    }
}
