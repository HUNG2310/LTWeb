using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WorkManager.Data;
using WorkManager.Models;
using System.Linq;
using System.Threading.Tasks;

namespace WorkManager.Controllers
{
    public class SettingsController : Controller
    {
        private readonly AppDbContext _context;

        public SettingsController(AppDbContext context)
        {
            _context = context;
        }

        // Hiển thị trang Cài đặt (có nút Đổi mật khẩu)
        public IActionResult Index()
        {
            return View();
        }

        // Hiển thị form đổi mật khẩu
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword)
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login", "Account");

            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
                return NotFound();

            // Kiểm tra mật khẩu cũ
            if (user.Password != oldPassword)
            {
                TempData["ErrorMessage"] = "❌ Mật khẩu cũ không đúng!";
                return RedirectToAction(nameof(ChangePassword));
            }

            // Cập nhật mật khẩu mới
            user.Password = newPassword;
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "✅ Mật khẩu đã được thay đổi thành công!";
            
            return RedirectToAction(nameof(Index));
        }
    }
}