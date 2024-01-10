using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Services.AnnouncementService
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly DataContext _context;

        public AnnouncementService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Announcement>> GetAllAnnouncement()
        {
            var announcement = await _context.Announcements.ToListAsync();
            return announcement;
        }

        public async Task<Announcement?> GetSingleAnnouncement(int id)
        {
            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement == null) 
            {
                return null;
            }
            return announcement;
        }

        public async Task<List<Announcement>> AddAnnouncement(AnnouncementDTO request)
        {
            var newAnnouncement = new Announcement
            {
                Message = request.Message,
                DateCreated = DateTime.UtcNow,
            };

            _context.Announcements.Add(newAnnouncement);
            await _context.SaveChangesAsync();
            return await _context.Announcements.ToListAsync();

        }
        public async Task<List<Announcement>?> DeleteAnnouncement(int id)
        {
            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement is null)
                return null;

            _context.Announcements.Remove(announcement);
            await _context.SaveChangesAsync();

            return await _context.Announcements.ToListAsync();
        }

        public async Task<List<Announcement>?> UpdateAnnouncement(int id, AnnouncementDTO request)
        {
            var announcement = await _context.Announcements
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
            if (announcement is null)
                return null;

            announcement.Message = request.Message;
            announcement.DateCreated = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await _context.Announcements.ToListAsync();
        }
    }
    
}
