using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WorkManager.Data;
using WorkManager.Models;
using System.Linq;
using System.Threading.Tasks;

namespace WorkManager.Controllers
{
    public class TaskController : Controller
    {
        private readonly AppDbContext _context;

        public TaskController(AppDbContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách công việc và form thêm mới
        public IActionResult Index()
        {
            // Kiểm tra đăng nhập qua Session (nếu chưa đăng nhập chuyển hướng về trang Login).
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
                _context.Tasks.Add(model);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Thêm công việc thành công!";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Dữ liệu không hợp lệ, vui lòng kiểm tra lại.";
            return RedirectToAction(nameof(Index));
        }

        // Xoá công việc
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

        // Action chỉnh sửa phần trăm hoàn thành
        [HttpPost]
        public async Task<IActionResult> EditCompletion(int id, int completionPercentage)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound();

            task.CompletionPercentage = completionPercentage;

            if (completionPercentage == 100)
            {
                task.IsCompleted = true;
                TempData["SuccessMessage"] = $"Công việc '{task.Name}' đã hoàn thành!";
            }
            else
            {
                task.IsCompleted = false;
                TempData["SuccessMessage"] = $"Đã cập nhật % hoàn thành của '{task.Name}' thành {completionPercentage}%";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}