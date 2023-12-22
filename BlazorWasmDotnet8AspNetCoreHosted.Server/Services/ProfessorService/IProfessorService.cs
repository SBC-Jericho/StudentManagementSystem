using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Services.ProfessorService
{
    public interface IProfessorService
    {
        Task<List<Professor>> GetAllProfessor();
        Task<Professor?> GetSingleProfessor(int id);
        Task<List<Professor>> AddProfessor(ProfessorDTO hero);
        Task<List<Professor>?> UpdateProfessor(int id, ProfessorDTO request);
        Task<List<Professor>?> DeleteProfessor(int id);
    }
}
