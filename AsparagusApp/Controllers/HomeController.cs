using AsparagusApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AsparagusApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationContext db;

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            this.db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Report()
        {
            var report = db.AsparagusLogs.GroupBy(x => new { x.Email, x.Name }).Select(x => new AsparagusLogViewModel
            { 
                Name = x.Key.Name,
                Email = x.Key.Email,
                LastDate = x.Max(r=>r.AddDate), 
                Count = x.Count()
            }).ToList();
            return View(report);
        }

        [HttpPost]
        public async Task<IActionResult> Index(AsparagusLog model)
        {
            if (ModelState.IsValid)
            {
                model.AddDate = DateTime.Now;
                db.AsparagusLogs.Add(model);
                await db.SaveChangesAsync();
                return RedirectToAction("Report");
            }
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}