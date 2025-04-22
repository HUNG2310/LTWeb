using Microsoft.AspNetCore.Mvc;
using WorkManager.Data;
using WorkManager.Models;
using System.Linq;

namespace WorkManager.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly AppDbContext _context;

        public StatisticsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Lấy số liệu từ bảng Tasks
            var totalTasks = _context.Tasks.Count();
            var completedTasks = _context.Tasks.Count(t => t.IsCompleted);
            var pendingTasks = totalTasks - completedTasks;
         
            double averageCompletion = (totalTasks > 0)
                ? _context.Tasks.Average(t => t.CompletionPercentage)
                : 0;

            // Tạo view model với các số liệu thống kê
            var viewModel = new StatisticsViewModel
            {
                TotalTasks = totalTasks,
                CompletedTasks = completedTasks,
                PendingTasks = pendingTasks,
                AverageCompletion = averageCompletion
            };

            return View(viewModel);
        }
    }
}