using WorkManager.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình DbContext với SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Thêm Session service
builder.Services.AddSession();

// Thêm MVC
builder.Services.AddControllersWithViews();

// Build app
var app = builder.Build();

// Xử lý lỗi
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

// Middleware
app.UseStaticFiles();
app.UseRouting();

// ✅ Đặt UseSession sau UseRouting nhưng trước Authorization
app.UseSession();
app.UseAuthorization();

// Cấu hình route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}"); // ✅ Nếu bạn muốn mặc định là trang đăng nhập

app.Run();
