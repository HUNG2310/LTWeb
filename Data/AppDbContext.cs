using Microsoft.EntityFrameworkCore;
using WorkManager.Models;

namespace WorkManager.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor nhận DbContextOptions để cấu hình DbContext.
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Các DbSet đại diện cho các bảng trong database.
        public DbSet<Board> Boards { get; set; } = null!;
        public DbSet<TaskModel> Tasks { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Template> Templates { get; set; } = null!;

        // Phương thức OnModelCreating dùng để cấu hình schema và seed dữ liệu mẫu.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Seed dữ liệu mẫu cho bảng Template
            modelBuilder.Entity<Template>().HasData(
                new Template { Id = 1, Name = "Lập trình web", Description = "Thiết kế website responsive, MVC" },
                new Template { Id = 2, Name = "Lập trình app", Description = "Xây dựng ứng dụng di động (Flutter/Xamarin)" }
            );
        }
    }
}