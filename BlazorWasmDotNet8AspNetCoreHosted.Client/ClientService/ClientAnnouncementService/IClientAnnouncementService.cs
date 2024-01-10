using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;

namespace BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientAnnouncementService
{
    public interface IClientAnnouncementService
    {
        List<Announcement> ClientAnnouncement { get; set;}
        Task<List<Announcement>> GetAllAnnouncement();
        Task<Announcement?> GetSingleAnnouncement(int id);
        Task AddAnnouncement(AnnouncementDTO request);
        Task UpdateAnnouncement(int id, Announcement request);
        Task DeleteAnnouncement(int id);
    }
}
