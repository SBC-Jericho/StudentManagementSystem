using BlazorWasmDotnet8AspNetCoreHosted.Server.Services.AnnouncementService;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;

        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Announcement>>> GetAllAnnouncement()
        {

            return await _announcementService.GetAllAnnouncement();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Announcement>> GetSingleAnnouncement(int id)
        {
            var result = await _announcementService.GetSingleAnnouncement(id);
            if (result is null)
                return NotFound("Announcement Not Found");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Announcement>>> AddAnnouncement(AnnouncementDTO request)
        {
            var result = await _announcementService.AddAnnouncement(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Announcement>> UpdateAnnouncement(int id, AnnouncementDTO request)
        {
            var result = await _announcementService.UpdateAnnouncement(id, request);
            if (result is null)
                return NotFound("Announcement Not Found");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Announcement>> DeleteAnnouncement(int id)
        {
            var result = await _announcementService.DeleteAnnouncement(id);
            if (result is null)
                return NotFound("Announcement Not Found");

            return Ok(result);
        }
    }
}
