using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Padrinly.Data;
using Padrinly.Domain.Entities;

namespace Padrinly.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Person
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Persons.Include(p => p.Institution).Include(p => p.Responsible).Include(p => p.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Person/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .Include(p => p.Institution)
                .Include(p => p.Responsible)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: Person/Create
        public IActionResult Create()
        {
            ViewData["IdInstitution"] = new SelectList(_context.Persons, "Id", "Address");
            ViewData["IdResponsible"] = new SelectList(_context.Persons, "Id", "Address");
            ViewData["IdUser"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Person/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email,PhoneNumber,BirthDate,IsAnonimous,AvatarFileName,AvatarInternalName,Type,FirstDocument,SecondDocument,Address,Neighborhood,City,State,PostalCode,Number,Complement,CreatedBy,UpdatedBy,CreatedAt,UpdatedAt")] Person person)
        {
            if (ModelState.IsValid)
            {
                person.BirthDate = person.BirthDate.ToUniversalTime();
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdInstitution"] = new SelectList(_context.Persons, "Id", "Address", person.IdInstitution);
            ViewData["IdResponsible"] = new SelectList(_context.Persons, "Id", "Address", person.IdResponsible);
            ViewData["IdUser"] = new SelectList(_context.Users, "Id", "Id", person.IdUser);
            return View(person);
        }

        // GET: Person/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            ViewData["IdInstitution"] = new SelectList(_context.Persons, "Id", "Address", person.IdInstitution);
            ViewData["IdResponsible"] = new SelectList(_context.Persons, "Id", "Address", person.IdResponsible);
            ViewData["IdUser"] = new SelectList(_context.Users, "Id", "Id", person.IdUser);
            return View(person);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Email,PhoneNumber,BirthDate,IsAnonimous,AvatarFileName,AvatarInternalName,TypePerson,IdResponsible,IdInstitution,FirstDocument,SecondDocument,IdUser,Address,Neighborhood,City,State,PostalCode,Number,Complement,Id,CreatedBy,UpdatedBy,CreatedAt,UpdatedAt")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdInstitution"] = new SelectList(_context.Persons, "Id", "Address", person.IdInstitution);
            ViewData["IdResponsible"] = new SelectList(_context.Persons, "Id", "Address", person.IdResponsible);
            ViewData["IdUser"] = new SelectList(_context.Users, "Id", "Id", person.IdUser);
            return View(person);
        }

        // GET: Person/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .Include(p => p.Institution)
                .Include(p => p.Responsible)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person != null)
            {
                _context.Persons.Remove(person);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.Persons.Any(e => e.Id == id);
        }
    }
}
