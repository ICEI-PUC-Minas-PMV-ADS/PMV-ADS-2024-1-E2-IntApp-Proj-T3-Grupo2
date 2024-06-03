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
        private readonly SignInManager<User> _signInManager;
        private string _filePath;

        public PersonController(ApplicationDbContext context, UserManager<User> userManager, SignInManager<User> signInManager, IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _filePath = env.WebRootPath;
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

            var listPost = await _context.Posts
                .Where(p => p.IdUser == id)
                .ToListAsync();

            ViewBag.ListPost = listPost;

            var person = await _context.Persons
                .Include(p => p.Institution)
                .Include(p => p.Responsible)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.IdUser == id);

            var userId = User.GetUserId();

            if(User.IsInRole("Institution") || User.IsInRole("Patron"))
            {
                return View(person);
            }

            if (person.IdUser != userId)
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
        public async Task<IActionResult> Create([Bind("Name,Email,Password,PhoneNumber,BirthDate,IsAnonimous,AvatarFileName,AvatarInternalName,Type,FirstDocument,SecondDocument,Address,Neighborhood,City,State,PostalCode,Number,Complement,CreatedBy,UpdatedBy,CreatedAt,UpdatedAt")] Person person, IFormFile? avatarFile)
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

                if (avatarFile != null)
                {
                    var name = SaveFile(avatarFile);
                    person.AvatarFileName = name;
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
        public async Task<IActionResult> CreateStudent(StudentResponsibleViewModel model, IFormFile? avatarFileName)
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

            await _userManager.CreateAsync(studentUser, model.Password);

            if (avatarFileName != null)
            {
                var name = SaveFile(avatarFileName);
                model.AvatarFileName = name;
            }


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
                AvatarFileName = model.AvatarFileName
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
        public async Task<IActionResult> CreatePatron(Person person, IFormFile avatarFile)
        {
            int userIdLoged = User.GetUserId();
            var getUser = await _context.Users.FindAsync(userIdLoged);
            var isPatron = await _context.Persons.FirstOrDefaultAsync(p => p.IdUser == userIdLoged);

            if (isPatron == null)
            {
                if (avatarFile != null)
                {
                    var name = SaveFile(avatarFile);
                    person.AvatarFileName = name;
                }

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
                    AvatarFileName = person.AvatarFileName
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
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Person person, IFormFile? avatarFile)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (person.Password == null)
            {
                ModelState.ClearValidationState("Password");
                ModelState.MarkFieldValid("Password");
            }

            if (!ModelState.IsValid && avatarFile == null || ModelState.IsValid)
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
                        if (person.Password != null)
                        {
                            await _userManager.RemovePasswordAsync(user);
                            await _userManager.AddPasswordAsync(user, person.Password);
                        }

                        await _context.SaveChangesAsync();
                    }
                    if (avatarFile != null)
                    {
                        string filePathName = _filePath + "\\images\\" + person.AvatarFileName;

                        if (avatarFile.ToString() != person.AvatarFileName)
                        {
                            if(System.IO.File.Exists(filePathName))
                                System.IO.File.Delete(filePathName);
                        }

                        var name = SaveFile(avatarFile);
                        person.AvatarFileName = name;
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
                if (person.Type == TypePerson.Patron)
                {
                    return RedirectToAction("Index", "Home");
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

            if (person.Type == TypePerson.Institution)
            {
                ViewBag.ConfirmInstitution = "Tem certeza que deseja excluir o registro abaixo?";
            }

            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == person.IdUser);

            var studentList = await _context.Persons.Where(p => p.IdResponsible == person.Id)
                .ToListAsync();

            var personsList = await _context.Persons.Where(p => p.IdInstitution == person.Id)
                .ToListAsync();

            if (personsList.Any())
            {
                foreach (var persons in personsList)
                {
                    var responsibles = await _context.Persons.Where(r => r.Id == persons.IdResponsible)
                        .ToListAsync();

                    var studentUser = await _context.Users.FirstOrDefaultAsync(su => su.Id == persons.IdUser);

                    foreach (var responsible in responsibles)
                    {
                        _context.Persons.Remove(responsible);
                    }

                    string filePathStudent = _filePath + "\\images\\" + persons.AvatarFileName;
                    if (System.IO.File.Exists(filePathStudent))
                        System.IO.File.Delete(filePathStudent);

                    _context.Persons.Remove(persons);
                    _context.Users.Remove(studentUser);
                }

                string filePathInstitution = _filePath + "\\images\\" + person.AvatarFileName;
                if (System.IO.File.Exists(filePathInstitution))
                    System.IO.File.Delete(filePathInstitution);

                _context.Persons.Remove(person);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            if (studentList.Any())
            {
                ViewBag.StudentList = studentList;
                return View(person);
            }

            if (user != null)
            {
                string filePath = _filePath + "\\images\\" + person.AvatarFileName;
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                _context.Persons.Remove(person);
                _context.Users.Remove(user);
            }
            else
            {
                _context.Persons.Remove(person);
            }

            await _context.SaveChangesAsync();

            if (User.IsInRole("Patron"))
            {
                var userId = User.GetUserId();

                var verifyUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (verifyUser == null)
                {
                    await _signInManager.SignOutAsync();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public string SaveFile(IFormFile avatarFile)
        {
            var extension = Path.GetExtension(avatarFile.FileName);
            var name = $"{Guid.NewGuid()}{extension}";


            var filePath = _filePath + "\\images";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            using (var stream = System.IO.File.Create(filePath + "\\" + name))
            {
                avatarFile.CopyToAsync(stream);
            }

            return name;
        }

        private bool PersonExists(int id)
        {
            return _context.Persons.Any(e => e.Id == id);
        }

        public async Task<IActionResult> CreatedPost(Post model)
        {
            var userId = User.GetUserId();
            
            var post = new Post 
            { 
                IdUser = userId,
                CreatedAt = DateTime.UtcNow,
                Content = model.Content,
                IsFixed = false,
            };

            _context.Add(post);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
