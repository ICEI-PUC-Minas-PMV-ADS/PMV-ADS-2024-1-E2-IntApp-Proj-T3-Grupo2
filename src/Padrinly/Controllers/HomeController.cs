using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Padrinly.Common.Extensions;
using Padrinly.Data;
using Padrinly.Domain.Enums;
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
        public async Task<IActionResult> Index()
        {
            var userId = User.GetUserId();

            ViewBag.UserId = userId;

            var user = await _context.Persons
                .FirstOrDefaultAsync(p => p.IdUser == userId);

            if(user != null) 
            {
                if (user.Type == TypePerson.Patron)
                {
                    var patronList = await _context.PersonPatrons
                        .Where(pp => pp.IdPatron == user.IdUser)
                        .ToListAsync();

                    var person = _context.Persons.FirstOrDefault(p => p.IdUser == userId);
                    ViewBag.IsPatron = true;
                    ViewBag.PersonId = person.Id;

                    var studentIds = patronList.Select(pp => pp.IdStudent).ToList();

                    var students = await _context.Persons
                        .Where(s => studentIds.Contains(s.IdUser.Value))
                        .ToListAsync();

                    ViewBag.PatronList = students;
                }
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
