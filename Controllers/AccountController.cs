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
                // Lưu thông tin vào session
                HttpContext.Session.SetString("IsLoggedIn", "true");
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("FullName", user.FullName);
                HttpContext.Session.SetString("Email", user.Email);
                HttpContext.Session.SetString("PhoneNumber", user.PhoneNumber);
                HttpContext.Session.SetString("DateOfBirth", user.DateOfBirth.ToString("yyyy-MM-dd"));
                HttpContext.Session.SetString("Position", user.Position);
                HttpContext.Session.SetString("Gender", user.Gender);
                
                // Trả về trang chính
                return RedirectToAction("Index", "Task");
            }

            ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu";
            return View();
        }
        public IActionResult Logout()
        {
            // Xóa session khi đăng xuất
            HttpContext.Session.Remove("IsLoggedIn");
            return RedirectToAction("Login");
        }
    }
}
