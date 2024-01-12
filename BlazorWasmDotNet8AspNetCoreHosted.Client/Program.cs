global using Blazored.LocalStorage;
global using Microsoft.AspNetCore.Components.Authorization;
using BlazorWasmDotNet8AspNetCoreHosted.Client;
using BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientAnnouncementService;
using BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientAuthService;
using BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientBookService;
using BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientBorrowedBookService;
using BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientChatMessageService;
using BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientEnrolledSubjectsService;
using BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientProfessorService;
using BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientStudentService;
using BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientSubjectService;
using BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientUserService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;



var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IClientStudentService, ClientStudentService>();
builder.Services.AddScoped<IClientProfessorService, ClientProfessorService>();
builder.Services.AddScoped<IClientSubjectService, ClientSubjectService>();
builder.Services.AddScoped<IClientBookService, ClientBookService>();
builder.Services.AddScoped<IClientAuthService, ClientAuthService>();
builder.Services.AddScoped<IClientEnrolledSubjectsService, ClientEnrolledSubjectsService>();
builder.Services.AddScoped<IClientBorrowedBookService, ClientBorrowedBookService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<IClientUserService, ClientUserService>();
builder.Services.AddScoped<IClientAnnouncementService, ClientAnnouncementService>();
builder.Services.AddScoped<IClientChatMessageService, ClientChatMessageService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices();


await builder.Build().RunAsync();
