using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientEnrolledSubjectsService
{
    public class ClientEnrolledSubjectsService : IClientEnrolledSubjectsService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        private readonly ISnackbar _snackbar;

        public ClientEnrolledSubjectsService(HttpClient http, NavigationManager navigationManager, ISnackbar snackbar)
        {
            _http = http;
            _navigationManager = navigationManager;
            _snackbar = snackbar;
        }
        public List<EnrolledSubjects> ClientEnrolledSubjects { get; set; } = new List<EnrolledSubjects>();

        public async Task<List<EnrolledSubjects>> GetSingleEnrolledSubjects(int id) 
        {
            var result = await _http.GetFromJsonAsync<List<EnrolledSubjects>>($"api/Enrolledsubject/enrollment-details/{id}");
            if(result == null) 
            {
                ClientEnrolledSubjects = result;
            }
            return result;
        }

        public async Task<List<EnrolledSubjects>> GetAllEnrolledSubject()
        {
            var result = await _http.GetFromJsonAsync<List<EnrolledSubjects>>("api/Enrolledsubject/");
            if (result != null)
            {
                ClientEnrolledSubjects = result;
            }
            return ClientEnrolledSubjects;
        }

        public async Task<int> AddEnrolledSubject(EnrollmentDTO enroll)
        {
            // Controller end point
            HttpResponseMessage? status = await _http.PostAsJsonAsync("api/enrolledsubject/add-enrolled-sub", enroll);
            // Razor page endpoint
            if (status.IsSuccessStatusCode)
            {
                _snackbar.Add(
                   "Subject Successfully Enrolled",
                   Severity.Success,
                   config =>
                   {
                       config.ShowTransitionDuration = 200;
                       config.HideTransitionDuration = 400;
                       config.VisibleStateDuration = 2500;
                   });

                _navigationManager.NavigateTo("all-enrolledsubject");
            }
            else
            {
                _snackbar.Add(
                   "Subject Already Enrolled",
                   Severity.Warning,
                   config =>
                   {
                       config.ShowTransitionDuration = 200;
                       config.HideTransitionDuration = 400;
                       config.VisibleStateDuration = 2500;
                   });
                return 0;
            }

            return 0;

        }
    }
}
