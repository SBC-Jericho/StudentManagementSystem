using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;

namespace BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientAnnouncementService
{
    public class ClientAnnouncementService : IClientAnnouncementService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public ClientAnnouncementService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public List<Announcement> ClientAnnouncement { get; set; } = new List<Announcement>();

        //public async Task<List<Announcement>> AddAnnouncement(Announcement context)
        //{
        //    HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/Announcement", context);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        announcements = await response.Content.ReadFromJsonAsync<List<Announcement>>();
        //        return announcements;
        //    }
        //    else
        //    {
        //        return new List<Announcement>() { };
        //    }
        //}
        public async Task AddAnnouncement(AnnouncementDTO request)
        {
            // Controller end point
            var  result = await _http.PostAsJsonAsync("api/Announcement/", request);

            if (result.IsSuccessStatusCode)
            {
                List<Announcement>? content = await result.Content.ReadFromJsonAsync<List<Announcement>>();
                if (content != null)
                    ClientAnnouncement = content;
            }

        }

        public async Task DeleteAnnouncement(int id)
        {
            var result = await _http.DeleteAsync($"api/Announcement/{id}");


            if (result.IsSuccessStatusCode)
            {
                List<Announcement>? content = await result.Content.ReadFromJsonAsync<List<Announcement>>();
                if (content != null) ClientAnnouncement = content;
            }
        }

        public async Task<List<Announcement>> GetAllAnnouncement()
        {
            var result = await _http.GetFromJsonAsync<List<Announcement>>("api/Announcement/");
            if (result != null)
            {
                ClientAnnouncement = result;
            }
            return ClientAnnouncement;
        }

        public async Task<Announcement?> GetSingleAnnouncement(int id)
        {
            // if provided an Id that does not exist GetAsync returns null
            var result = await _http.GetAsync($"api/Announcement/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<Announcement>();
            }
            return null;
        }

        public async Task UpdateAnnouncement(int id, Announcement request)
        {
            await _http.PutAsJsonAsync($"api/Announcement/{id}", request);
            _navigationManager.NavigateTo("all-announcement");
        }
    }
}
