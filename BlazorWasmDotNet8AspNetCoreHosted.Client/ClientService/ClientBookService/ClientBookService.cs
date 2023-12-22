using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Net;

namespace BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientBookService
{
    public class ClientBookService : IClientBookService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public ClientBookService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public List<Book> ClientBook { get; set; } = new List<Book>();

        public async Task AddBook(Book Book)
        {
            // Controller end point
            await _http.PostAsJsonAsync("api/Book/", Book);
            // Razor page endpoint
            _navigationManager.NavigateTo("all-book");
        }

        public async Task DeleteBook(int id)
        {
            var result = await _http.DeleteAsync($"api/Book/{id}");


            if (result.IsSuccessStatusCode)
            {
                List<Book>? content = await result.Content.ReadFromJsonAsync<List<Book>>();
                if (content != null) ClientBook = content;
            }
        }

        public async Task<List<Book>> GetAllBook()
        {
            var result = await _http.GetFromJsonAsync<List<Book>>("api/Book/");
            if (result != null)
            {
                ClientBook = result;
            }
            return ClientBook;
        }

        public async Task<Book?> GetSingleBook(int id)
        {
            // if provided an Id that does not exist GetAsync returns null
            var result = await _http.GetAsync($"api/Book/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<Book>();
            }
            return null;
        }

        public async Task UpdateBook(int id, Book request)
        {
            await _http.PutAsJsonAsync($"api/Book/{id}", request);
            _navigationManager.NavigateTo("all-book");
        }
    }
}
