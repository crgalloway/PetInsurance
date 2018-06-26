using System;
using ProblemD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProblemD
{
    public class PetOwnerController : Controller
    {
        private ProblemDContext _context;
        public PetOwnerController(ProblemDContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            petOwnerRegistration regForm = new petOwnerRegistration();
            regForm.AllCountries = _context.country.ToList();
            ViewBag.RegForm = regForm;
            return View();
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register(petOwnerRegistration freshRegistration)
        {
            if(ModelState.IsValid)
            {
                PasswordHasher<PetOwner> hasher = new PasswordHasher<PetOwner>();
                PetOwner newPetOwner = new PetOwner{
                    Name = freshRegistration.Name,
                    Email = freshRegistration.Email,
                    Password = freshRegistration.Password,
                    CountryId = freshRegistration.CountryId,
                    EnrollmentDate = DateTime.Now,
                    Active = true
                };
                newPetOwner.Password = hasher.HashPassword(newPetOwner ,newPetOwner.Password);
                _context.Add(newPetOwner);
                _context.SaveChanges();
                int userId = _context.petowner.Single( u => u.Email == newPetOwner.Email).Id;
                HttpContext.Session.SetInt32("activeUser", userId);
                return RedirectToAction("Dashboard");
            }
            petOwnerRegistration regForm = new petOwnerRegistration();
            regForm.AllCountries = _context.country.ToList();
            ViewBag.RegForm = regForm;
            return View("Index");
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(PetOwnerLogin loginAttempt)
        {
            if(ModelState.IsValid)
            {
                int userId = _context.petowner.Single( u => u.Email == loginAttempt.LoginEmail).Id;
                HttpContext.Session.SetInt32("activeUser", userId);
                return RedirectToAction("Dashboard");
            }
            petOwnerRegistration regForm = new petOwnerRegistration();
            regForm.AllCountries = _context.country.ToList();
            ViewBag.RegForm = regForm;
            return View("Index");
        }
        [HttpGet]
        [Route("Dashoard")]
        public IActionResult Dashboard()
        {
            int? activeId = HttpContext.Session.GetInt32("activeUser");
            if(activeId == null)
            {
                return RedirectToAction("Index");
            }
            PetOwner activeUser = _context.petowner.Include( o => o.CountryOfResidence ).Include( o => o.OwnedPets ).ThenInclude( p => p.Breed ).Single( o => o.Id == (int)activeId);
            return View(activeUser);
        }
        [HttpGet]
        [Route("CancelPolicy/{id}")]
        public IActionResult CancelPolicy(int id)
        {
            int? activeId = HttpContext.Session.GetInt32("activeUser");
            if(activeId == null || id != (int)activeId)
            {
                return RedirectToAction("Index");
            }
            PetOwner activeUser = _context.petowner.Single( o => o.Id == id);
            activeUser.Active = false;
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [HttpGet]
        [Route("ActivatePolicy/{id}")]
        public IActionResult ActivatePolicy(int id)
        {
            int? activeId = HttpContext.Session.GetInt32("activeUser");
            if(activeId == null || id != (int)activeId)
            {
                return RedirectToAction("Index");
            }
            PetOwner activeUser = _context.petowner.Single( o => o.Id == id);
            activeUser.Active = true;
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
    }
}