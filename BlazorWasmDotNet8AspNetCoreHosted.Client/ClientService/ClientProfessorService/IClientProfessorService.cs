using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;

namespace BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientProfessorService
{
    public interface IClientProfessorService
    {
        List<Professor> ClientProfessor { get; set; }
        Task<List<Professor>> GetAllProfessor();
        Task<Professor?> GetSingleProfessor(int id);
        Task AddProfessor(Professor hero);
        Task UpdateProfessor(int id, Professor request);
        Task DeleteProfessor(int id);
    }
}
