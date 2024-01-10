using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Services.AnnouncementService
{
    public interface IAnnouncementService
    {
        Task<List<Announcement>> GetAllAnnouncement();
        Task<Announcement?> GetSingleAnnouncement(int id);
        Task<List<Announcement>> AddAnnouncement(AnnouncementDTO request);

        Task<List<Announcement>?> UpdateAnnouncement(int id, AnnouncementDTO request);
        Task<List<Announcement>?> DeleteAnnouncement(int id);
    }
}
