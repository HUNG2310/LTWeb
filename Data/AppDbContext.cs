using Microsoft.EntityFrameworkCore;
using WorkManager.Models;

namespace WorkManager.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<TaskModel> Tasks { get; set; }
        // Các DbSet khác nếu cần
        public DbSet<Board> Boards { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Template> Templates { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Ví dụ seed dữ liệu cho Template
            modelBuilder.Entity<Template>().HasData(
                new Template { Id = 1, Name = "Lập trình web", Description = "Thiết kế website responsive, MVC" },
                new Template { Id = 2, Name = "Lập trình app", Description = "Xây dựng ứng dụng di động (Flutter/Xamarin)" }
            );
        }
    }
}