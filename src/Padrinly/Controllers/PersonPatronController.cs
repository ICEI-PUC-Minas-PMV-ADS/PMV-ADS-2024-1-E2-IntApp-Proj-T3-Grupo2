using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Padrinly.Common.Extensions;
using Padrinly.Data;
using Padrinly.Domain.Entities;
using Padrinly.Domain.Enums;

namespace Padrinly.Controllers
{
    public class PersonPatronController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public PersonPatronController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Affiliate()
        {
            var students = await _context.Persons
                .Where(s => s.Type == TypePerson.Student)
                .ToListAsync();

            var rnd = new Random();
            var randomStudent = students[rnd.Next(students.Count)];

            var userId = User.GetUserId();

            var patron = await _context.Persons
                .FirstOrDefaultAsync(p => p.IdUser == userId);

            var patronList = await _context.PersonPatrons
                .Where(pp => pp.IdPatron == patron.IdUser)
                .ToListAsync();

            while (patronList.Any(pp => pp.IdStudent == randomStudent.Id))
            {
                randomStudent = students[rnd.Next(students.Count)];
            }
            var personPatron = new PersonPatron
            {
                IdPatron = patron.IdUser.Value,
                IdStudent = randomStudent.IdUser.Value,
            };

            _context.Add(personPatron);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
