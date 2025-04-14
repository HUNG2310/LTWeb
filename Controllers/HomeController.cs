using Microsoft.AspNetCore.Mvc;
using WorkManager.Data;
using WorkManager.Models;
using System.Linq;

namespace WorkManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var boards = _context.Boards.ToList();
            return View(boards);
        }

        [HttpPost]
        public IActionResult AddBoard(string boardName)
        {
            if (!string.IsNullOrWhiteSpace(boardName))
            {
                _context.Boards.Add(new Board { Name = boardName });
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
