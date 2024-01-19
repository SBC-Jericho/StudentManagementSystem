global using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
global using BlazorWasmDotnet8AspNetCoreHosted.Server.Data;
using BlazorWasmDotnet8AspNetCoreHosted.Server.Services.AuthService;
using BlazorWasmDotnet8AspNetCoreHosted.Server.Services.BookService;
using BlazorWasmDotnet8AspNetCoreHosted.Server.Services.ProfessorService;
using BlazorWasmDotnet8AspNetCoreHosted.Server.Services.StudentService;
using BlazorWasmDotnet8AspNetCoreHosted.Server.Services.SubjectService;
using BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientAuthService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BlazorWasmDotnet8AspNetCoreHosted.Server.Services.EnrolledSubjectsService;
using BlazorWasmDotnet8AspNetCoreHosted.Server.Services.BorrowedBookService;
using BlazorWasmDotnet8AspNetCoreHosted.Server.Services.UserService;
using BlazorWasmDotnet8AspNetCoreHosted.Server.Hubs;
using BlazorWasmDotnet8AspNetCoreHosted.Server.Services.AnnouncementService;
using BlazorWasmDotnet8AspNetCoreHosted.Server.Services.ChatMessageService;
using BlazorWasmDotnet8AspNetCoreHosted.Server.Services.GroupChatService;

var builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("SMSConnection") ?? throw new InvalidOperationException("Connection string 'SMSConnection' not found.");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        })
    ;



// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProfessorService, ProfessorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IEnrolledSubjectsService, EnrolledSubjectsService>();
builder.Services.AddScoped<IBorrowedBookService, BorrowedBookService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IChatMessageService, ChatMessageService>();
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
builder.Services.AddScoped<IGroupChatService, GroupChatService>();
builder.Services.AddDbContext<DataContext>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSignalR();

builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //Debug the server and front-end both at the same time
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.MapHub<AnnouncementHub>("/announcementhub");
app.MapHub<ChatHub>("/chathub");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
