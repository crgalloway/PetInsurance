using System;
using ProblemD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProblemD
{
    //This Controller is used for logic that applies to PetOwners/users, such as registration, logging in, and rendering the Dashboard
    public class PetOwnerController : Controller
    {
        //Brings access to the database through the context model
        private ProblemDContext _context;
        public PetOwnerController(ProblemDContext context)
        {
            _context = context;

        }
        [HttpGet]
        [Route("")]
        //Route to show the Login/Reg page
        public IActionResult Index()
        {
            PetOwnerRegistration regForm = new PetOwnerRegistration();
            regForm.AllCountries = _context.country.ToList(); //List of countries to populate the drop-down menu on the registration page
            ViewBag.RegForm = regForm;//Needed to bring the PetOwnerRegistration model to the registration partial page
            return View();
        }
        [HttpPost]
        [Route("RegisterError")]
        //Route to process registration
        public IActionResult Register(PetOwnerRegistration freshRegistration)
        {
            if(ModelState.IsValid)
            {
                PasswordHasher<PetOwner> hasher = new PasswordHasher<PetOwner>();
                PetOwner newPetOwner = new PetOwner{ //Transfer of ViewModel to Db Model
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
            PetOwnerRegistration regForm = new PetOwnerRegistration();//This process to re-render Index page with errors
            regForm.AllCountries = _context.country.ToList();
            ViewBag.RegForm = regForm;
            ViewBag.RegErrors = true;//Allows for registration form to appear correctly in this scenario
            return View("Index");
        }
        [HttpPost]
        [Route("LoginError")]
        //Route to process login attempt
        public IActionResult Login(PetOwnerLogin loginAttempt)
        {
            if(ModelState.IsValid)
            {
                int userId = _context.petowner.Single( u => u.Email == loginAttempt.LoginEmail).Id;
                HttpContext.Session.SetInt32("activeUser", userId);
                return RedirectToAction("Dashboard");
            }
            PetOwnerRegistration regForm = new PetOwnerRegistration();//This process to re-render Index page with errors
            regForm.AllCountries = _context.country.ToList();
            ViewBag.RegForm = regForm;
            return View("Index");
        }
        [HttpGet]
        [Route("Dashboard")]
        public IActionResult Dashboard()
        {
            int? activeId = HttpContext.Session.GetInt32("activeUser");
            bool isValid = SecurityCheck.CheckForActiveUser(activeId);
            if(isValid == false)
            {
                return RedirectToAction("Index");
            }
            PetValidation newPetModel = new PetValidation();
            newPetModel.AllBreeds = _context.breed.OrderByDescending( b => b.Name ).ToList();
            ViewBag.NewPetModel = newPetModel;//Model placed in ViewBag to allow for form to appear on rendered partial
            PetOwner activeUser = _context.petowner.Include( o => o.CountryOfResidence ).Include( o => o.OwnedPets ).ThenInclude( p => p.Breed ).Single( o => o.Id == (int)activeId);
            return View(activeUser);
        }
        [HttpGet]
        [Route("{change}Policy/{id}")]
        public IActionResult CancelPolicy(string change, int id)
        {
            int? activeId = HttpContext.Session.GetInt32("activeUser");
            bool isValid = SecurityCheck.CheckIfUserAuthorized(activeId, id);
            if(isValid == false)
            {
                //Redirected to Logout as user may be malicious
                return RedirectToAction("Logout");
            }
            PetOwner activeUser = _context.petowner.Include( o => o.OwnedPets ).Single( o => o.Id == id);
            //Switch case allows one route/method to handle different yet similar logic as needed
            switch(change)
            {
                case "Cancel":
                    activeUser.Active = false;
                    foreach(Pet pet in activeUser.OwnedPets)
                    {
                        pet.Active = false;
                    }
                    break;
                case "Activate":
                    activeUser.Active = true;
                    break;
            }
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [HttpGet]
        [Route("/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }
    }
}