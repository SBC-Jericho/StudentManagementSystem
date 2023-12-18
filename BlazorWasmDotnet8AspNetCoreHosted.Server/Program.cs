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
builder.Services.AddDbContext<DataContext>();


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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
