using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WorkManager.Data;
using WorkManager.Models;
using System.Linq;

namespace WorkManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                // ✅ Dùng session để lưu trạng thái đăng nhập
                HttpContext.Session.SetString("IsLoggedIn", "true");
                return RedirectToAction("Index", "Task");
            }

            ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu";
            return View();
        }

        public IActionResult Logout()
        {
            // ✅ Xóa session khi đăng xuất
            HttpContext.Session.Remove("IsLoggedIn");
            return RedirectToAction("Login");
        }
    }
}
