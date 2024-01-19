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
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<EnrolledSubjects> EnrolledSubjects { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<BorrowedBooks> BorrowedBooks { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<GroupChat> GroupChats { get; set; }
        public DbSet<GroupChatMessage> GroupChatMessages { get; set; }

    }
}
