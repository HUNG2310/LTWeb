using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WorkManager.Data;
using WorkManager.Models;
using System.Linq;

namespace WorkManager.Controllers
{
    public class TaskController : Controller
    {
        private readonly AppDbContext _context;

        public TaskController(AppDbContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách công việc
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsLoggedIn")))
                return RedirectToAction("Login", "Account");

            var tasks = _context.Tasks.ToList();
            return View(tasks);
        }

        // Thêm mới công việc
        [HttpPost]
        public IActionResult Create(TaskModel model)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsLoggedIn")))
                return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                // Sử dụng giá trị mặc định của model nếu không có giá trị nhập vào
                _context.Tasks.Add(model);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Thêm công việc thành công!";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "Dữ liệu không hợp lệ, vui lòng kiểm tra lại.";
            return RedirectToAction(nameof(Index));
        }

        // Xóa công việc
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
                TempData["DeletedMessage"] = $"Đã xóa công việc: {task.Name}";
            }
            else
            {
                TempData["ErrorMessage"] = "Không tìm thấy công việc để xóa.";
            }
            return RedirectToAction("Index");
        }

        // Đánh dấu công việc đã hoàn thành
        [HttpPost]
        public IActionResult Complete(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsLoggedIn")))
                return RedirectToAction("Login", "Account");

            var task = _context.Tasks.Find(id);
            if (task == null)
            {
                TempData["ErrorMessage"] = "Công việc không tồn tại.";
                return RedirectToAction("Index");
            }

            task.IsCompleted = true;
            task.Progress = 100;
            task.Status = "Completed";
            task.CompletionPercentage = 100;

            _context.SaveChanges();

            TempData["SuccessMessage"] = $"Công việc '{task.Name}' đã được hoàn thành!";
            return RedirectToAction("Index");
        }
    }
}