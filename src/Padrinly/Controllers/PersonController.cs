using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Padrinly.Data;
using Padrinly.Domain.Entities;
using Padrinly.Models;

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
                // Cria usuário antes de criar pessoa
                var user = new User
                {
                    UserName = person.Name,
                    Email = person.Email,
                    PhoneNumber = person.PhoneNumber
                };
                _context.Add(user);
                await _context.SaveChangesAsync();

                person.IdUser = user.Id;

                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdInstitution"] = new SelectList(_context.Persons, "Id", "Address", person.IdInstitution);
            ViewData["IdResponsible"] = new SelectList(_context.Persons, "Id", "Address", person.IdResponsible);
            ViewData["IdUser"] = new SelectList(_context.Users, "Id", "Id", person.IdUser);
            return View(person);
        }

        [HttpGet]
        //[Authorize(Roles = "Institution")]
        public async Task<IActionResult> CreateStudent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Institution")]
        public async Task<IActionResult> CreateStudent(StudentResponsibleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var studentUser = new User
                {
                    UserName = model.StudentName,
                    Email = model.ResponsibleEmail,
                    PhoneNumber = model.ResponsiblePhoneNumber,
                };
                _context.Add(studentUser);
                await _context.SaveChangesAsync();

                if (model.IsNewResponsible)
                {
                    var responsible = new Person
                    {
                        Name = model.ResponsibleName,
                        BirthDate = model.ResponsibleBirthDate,
                        Email = model.ResponsibleEmail,
                        PhoneNumber = model.ResponsiblePhoneNumber,
                        Address = model.Address,
                        Neighborhood = model.Neighborhood,
                        City = model.City,
                        State = model.State,
                        PostalCode = model.PostalCode,
                        Number = model.Number,
                        Complement = model.Complement,
                        Type = Domain.Enums.TypePerson.Responsabile,
                        FirstDocument = model.ResponsibleFirstDocument,
                        SecondDocument = model.ResponsibleSecondtDocument,
                    };

                    _context.Add(responsible);
                    await _context.SaveChangesAsync();

                    model.IdResponsible = responsible.Id;
                }

                var student = new Person
                {
                    Name = model.StudentName,
                    BirthDate = model.StudentBirthDate,
                    IdResponsible = model.IdResponsible,
                    Email = model.ResponsibleEmail,
                    PhoneNumber = model.ResponsiblePhoneNumber,
                    Address = model.Address,
                    Neighborhood = model.Neighborhood,
                    City = model.City,
                    State = model.State,
                    PostalCode = model.PostalCode,
                    Number = model.Number,
                    Complement = model.Complement,
                    Type = Domain.Enums.TypePerson.Student,
                    FirstDocument = model.StudentFirstDocument,
                    SecondDocument = model.StudentSecondtDocument,
                    //IdInstitution = ?
                };
                _context.Add(student);
                await _context.SaveChangesAsync();
            }

            return View(model);
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
