using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Padrinly.Common.Extensions;
using Padrinly.Data;
using Padrinly.Models;
using System.Diagnostics;

namespace Padrinly.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize(Roles = "Institution,Admin,Person,Student,Patron")]
        public IActionResult Index()
        {
            var userId = User.GetUserId();
            var isPatron = _context.Persons.Any(p => p.IdUser == userId);
            if (isPatron)
            {
                ViewBag.IsPatron = true;
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
