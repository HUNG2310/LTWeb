using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WorkManager.Data;
using WorkManager.Models;
using System.Linq;
using System.Threading.Tasks;

namespace WorkManager.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // Hiển thị thông tin cá nhân
        public IActionResult Profile()
        {
            var username = HttpContext.Session.GetString("Username") ?? "Guest";
            var fullName = HttpContext.Session.GetString("FullName") ?? "Chưa cập nhật";
            var email = HttpContext.Session.GetString("Email") ?? "Chưa có email";
            var phoneNumber = HttpContext.Session.GetString("PhoneNumber") ?? "Chưa có số điện thoại";
            var dateOfBirth = HttpContext.Session.GetString("DateOfBirth") ?? "Chưa cập nhật";
            var position = HttpContext.Session.GetString("Position") ?? "Chưa có chức vụ";
            var gender = HttpContext.Session.GetString("Gender") ?? "Không xác định";

            var user = new User
            {
                Username = username,
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                DateOfBirth = DateTime.Parse(dateOfBirth), // Chuyển chuỗi thành DateTime
                Position = position,
                Gender = gender
            };

            return View(user);
        }
        // Chỉnh sửa thông tin cá nhân
        public IActionResult EditProfile()
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login", "Account");

            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(User model)
        {
            var user = await _context.Users.FindAsync(model.Id);
            if (user == null)
                return NotFound();

            user.FullName = model.FullName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.DateOfBirth = model.DateOfBirth;
            user.Position = model.Position;
            user.Gender = model.Gender;

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Thông tin cá nhân đã được cập nhật!";
            return RedirectToAction(nameof(Profile));
        }
    }
}