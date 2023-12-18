using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
using Microsoft.EntityFrameworkCore;
using static MudBlazor.Colors;

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
