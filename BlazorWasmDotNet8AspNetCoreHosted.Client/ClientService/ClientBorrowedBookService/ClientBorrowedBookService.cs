using BlazorWasmDotNet8AspNetCoreHosted.Shared.DTOs;
using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientBorrowedBookService
{
    public class ClientBorrowedBookService : IClientBorrowedBookService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        private readonly ISnackbar _snackbar;

        public ClientBorrowedBookService(HttpClient http, NavigationManager navigationManager, ISnackbar snackbar)
        {
            _http = http;
            _navigationManager = navigationManager;
            _snackbar = snackbar;
        }
        public List<BorrowedBooks> ClientBorrowedBooks { get; set; } = new List<BorrowedBooks>();

        public async Task<int> AddBorrowedBooks(LibraryDTO request)
        {
            HttpResponseMessage? status = await _http.PostAsJsonAsync("api/BorrowedBooks/add-borrowed-book", request);

            if (status.IsSuccessStatusCode)
            {
                _snackbar.Add(
                   "Successfully Borrowed Book",
                   Severity.Success,
                   config =>
                   {
                       config.ShowTransitionDuration = 200;
                       config.HideTransitionDuration = 400;
                       config.VisibleStateDuration = 2500;
                   });

                _navigationManager.NavigateTo("all-borrowedBooks");
            }
            else
            {
                _snackbar.Add(
                   "You Already Borrow this Book!",
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
        public async Task<List<BorrowedBooks>> GetAllBorrowedBook()
        {
            var result = await _http.GetFromJsonAsync<List<BorrowedBooks>>("api/BorrowedBooks/");
            if (result != null) 
            {
                ClientBorrowedBooks = result;
            }
            return ClientBorrowedBooks;
        }

        public async Task<List<BorrowedBooks>> GetSingleBorrowedBooks(int id)
        {
            var result = await _http.GetFromJsonAsync<List<BorrowedBooks>>($"api/BorrowedBooks/book-details/{id}");
            if (result == null)
            {
                ClientBorrowedBooks = result;
            }
            return result;
        }
    }
}
