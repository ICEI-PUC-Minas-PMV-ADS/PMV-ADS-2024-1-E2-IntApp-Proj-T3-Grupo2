using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol;
using Padrinly.Common.Extensions;
using Padrinly.Data;
using Padrinly.Domain.Entities;
using Padrinly.Domain.Enums;
using Padrinly.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Padrinly.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public PersonController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Person
        [Authorize(Roles = "Institution")]
        public async Task<IActionResult> Index()
        {
            var students = _context.Persons.Include(p => p.User)
                                .Where(p => p.Type == TypePerson.Student);

            var responsibles = _context.Persons.Include(p => p.User)
                                .Where(p => p.Type == TypePerson.Responsabile);

            var combinedPersons = await students.Union(responsibles).ToListAsync();

            return View(combinedPersons);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> InstitutionIndex()
        {
            var allUsers = _context.Persons.Include(p => p.User)
                .Where(u => u.Type == TypePerson.Institution)
                .ToList();

            return View(allUsers);
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Name,Email,Password,PhoneNumber,BirthDate,IsAnonimous,AvatarFileName,AvatarInternalName,Type,FirstDocument,SecondDocument,Address,Neighborhood,City,State,PostalCode,Number,Complement,CreatedBy,UpdatedBy,CreatedAt,UpdatedAt")] Person person)
        {
            if (ModelState.IsValid)
            {
                // Cria usuário antes de criar Instituição
                var user = new User
                {
                    UserName = person.Email,
                    NormalizedUserName = person.Email.ToUpper(),
                    Email = person.Email,
                    NormalizedEmail = person.Email.ToUpper(),
                    PhoneNumber = person.PhoneNumber
                };

                var result = await _userManager.CreateAsync(user, person.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, person.Type.ToString().ToUpper());
                }

                person.IdUser = user.Id;
                person.Type = TypePerson.Institution;

                _context.Add(person);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(InstitutionIndex));
            }
            ViewData["IdInstitution"] = new SelectList(_context.Persons, "Id", "Address", person.IdInstitution);
            ViewData["IdResponsible"] = new SelectList(_context.Persons, "Id", "Address", person.IdResponsible);
            ViewData["IdUser"] = new SelectList(_context.Users, "Id", "Id", person.IdUser);
            return View(person);
        }

        [HttpGet]
        [Authorize(Roles = "Institution")]
        public async Task<IActionResult> CreateStudent()
        {
            var persons = await _context.Persons
                .Where(p => p.Type == TypePerson.Responsabile)
                .ToListAsync();

            ViewBag.ResponsibleList = persons
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                })
                .ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Institution")]
        public async Task<IActionResult> CreateStudent(StudentResponsibleViewModel model)
        {
            if (!ModelState.IsValid && model.IsNewResponsible)
            {
                return View(model);
            }

            if (model.IsNewResponsible)
            {
                await CreateNewResponsible(model);
            }
            else
            {
                var selectedResponsible = await _context.Persons.FindAsync(model.SelectedPersonId);
                ViewSelectedResponsible(model, selectedResponsible);
            }

            var studentUser = new User
            {
                UserName = model.StudentEmail,
                NormalizedUserName = model.StudentEmail.ToUpper(),
                Email = model.StudentEmail,
                NormalizedEmail = model.StudentEmail.ToUpper(),
                PhoneNumber = model.ResponsiblePhoneNumber,
            };

            var result = _userManager.CreateAsync(studentUser, model.Password);

            _context.Add(studentUser);
            await _context.SaveChangesAsync();

            int userIdLoged = User.GetUserId();
            var personIDLoged = await _context.Persons.FirstOrDefaultAsync(pi => pi.IdUser == userIdLoged);

            var student = new Person
            {
                Name = model.StudentName,
                BirthDate = model.StudentBirthDate,
                IdResponsible = model.IdResponsible,
                Email = model.StudentEmail,
                PhoneNumber = model.ResponsiblePhoneNumber,
                Address = model.Address,
                Neighborhood = model.Neighborhood,
                City = model.City,
                State = model.State,
                PostalCode = model.PostalCode,
                Number = model.Number,
                Complement = model.Complement,
                Type = TypePerson.Student,
                FirstDocument = model.StudentFirstDocument,
                SecondDocument = model.StudentSecondtDocument,
                IdUser = studentUser.Id,
                Password = model.Password,
                IdInstitution = personIDLoged.Id,
            };

            await _userManager.AddToRoleAsync(studentUser, student.Type.ToString().ToUpper());

            _context.Add(student);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task CreateNewResponsible(StudentResponsibleViewModel model)
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
                Type = TypePerson.Responsabile,
                FirstDocument = model.ResponsibleFirstDocument,
                SecondDocument = model.ResponsibleSecondtDocument,
                Password = model.Password
            };

            _context.Add(responsible);
            await _context.SaveChangesAsync();

            model.IdResponsible = responsible.Id;
        }

        private void ViewSelectedResponsible(StudentResponsibleViewModel model, Person selectedResponsible)
        {
            model.ResponsibleName = selectedResponsible.Name;
            model.ResponsibleEmail = selectedResponsible.Email;
            model.ResponsiblePhoneNumber = selectedResponsible.PhoneNumber;
            model.Address = selectedResponsible.Address;
            model.Neighborhood = selectedResponsible.Neighborhood;
            model.City = selectedResponsible.City;
            model.State = selectedResponsible.State;
            model.PostalCode = selectedResponsible.PostalCode;
            model.Number = selectedResponsible.Number;
            model.Complement = selectedResponsible.Complement;
            model.ResponsibleBirthDate = selectedResponsible.BirthDate;
            model.IdResponsible = selectedResponsible.Id;
        }

        [HttpGet]
        public async Task<IActionResult> CreatePatron()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePatron(Person person)
        {
            int userIdLoged = User.GetUserId();
            var getUser = await _context.Users.FindAsync(userIdLoged);
            var isPatron = await _context.Persons.FirstOrDefaultAsync(p => p.IdUser == userIdLoged);

            if(isPatron == null)
            {
                var patron = new Person
                {
                    Name = person.Name,
                    BirthDate = person.BirthDate,
                    Email = getUser.Email,
                    PhoneNumber = person.PhoneNumber,
                    Password = getUser.PasswordHash,
                    Address = person.Address,
                    Neighborhood = person.Neighborhood,
                    City = person.City,
                    State = person.State,
                    PostalCode = person.PostalCode,
                    Number = person.Number,
                    Complement = person.Complement,
                    Type = TypePerson.Patron,
                    FirstDocument = person.FirstDocument,
                    SecondDocument = person.SecondDocument,
                    IdUser = userIdLoged,
                };

                _context.Add(patron);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

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
            ViewBag.Password = person.Password.ToString().DefaultIfEmpty();

            ViewData["IdInstitution"] = new SelectList(_context.Persons, "Id", "Address", person.IdInstitution);
            ViewData["IdResponsible"] = new SelectList(_context.Persons, "Id", "Address", person.IdResponsible);
            ViewData["IdUser"] = new SelectList(_context.Users, "Id", "Id", person.IdUser);
            ViewData["Password"] = "";
            return View(person);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid && person.Password == null || ModelState.IsValid)
            {
                try
                {
                    var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == person.IdUser);

                    if (user != null)
                    {
                        user.UserName = person.Email;
                        user.NormalizedUserName = person.Email.ToUpper();
                        user.Email = person.Email;
                        user.NormalizedEmail = person.Email.ToUpper();
                        await _userManager.UpdateAsync(user);
                        if (person.Password != " ")
                        {
                            await _userManager.RemovePasswordAsync(user);
                            await _userManager.AddPasswordAsync(user, person.Password);
                        }

                        await _context.SaveChangesAsync();
                    }

                    _context.Update(person);
                    await _context.SaveChangesAsync();

                    if (person.Type == TypePerson.Responsabile)
                    {
                        var students = await _context.Persons.Where(p => p.IdResponsible == person.Id).ToListAsync();
                        foreach (var student in students)
                        {
                            student.PhoneNumber = person.PhoneNumber;
                            student.Address = person.Address;
                            student.Neighborhood = person.Neighborhood;
                            student.City = person.City;
                            student.State = person.State;
                            student.PostalCode = person.PostalCode;
                            student.Number = person.Number;
                            student.Complement = person.Complement;
                            _context.Update(student);
                        }
                    }
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
                if(person.Type == TypePerson.Patron)
                {
                    return RedirectToAction("Index", "Home");
                }
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
