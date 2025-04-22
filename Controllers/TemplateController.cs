using Microsoft.AspNetCore.Mvc;
using WorkManager.Data;
using WorkManager.Models;
using System.Linq;

namespace WorkManager.Controllers
{
    public class TemplateController : Controller
    {
        private readonly AppDbContext _context;

        public TemplateController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var list = _context.Templates.ToList();
            return View(list);
        }

        [HttpPost]
        public IActionResult Create(string name, string? description)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                _context.Templates.Add(new Template
                {
                    Name = name,
                    Description = description
                });
                _context.SaveChanges();
                TempData["SuccessMessage"] = "✅ Thêm mẫu công việc thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "❌ Tên mẫu công việc không được để trống.";
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var tpl = _context.Templates.Find(id);
            if (tpl != null)
            {
                _context.Templates.Remove(tpl);
                _context.SaveChanges();
                TempData["DeletedMessage"] = "🗑️ Đã xóa mẫu công việc.";
            }
            else
            {
                TempData["ErrorMessage"] = "❌ Không tìm thấy mẫu để xóa.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
