using System;
using ProblemD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProblemD
{
    public class PetController : Controller
    {
        private ProblemDContext _context;
        public PetController(ProblemDContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("Dashboard")]
        //Route to process new pet and maybe breed creation
        public IActionResult NewPet(PetValidation newPotentialPet)
        {
            int? activeId = HttpContext.Session.GetInt32("activeUser");
            bool isValid = SecurityCheck.CheckForActiveUser(activeId);
            if(isValid == false)
            {
                return RedirectToAction("Index", "PetOwner");
            }
            if(ModelState.IsValid)
            {
                if(newPotentialPet.BreedId == 0)
                {
                    var shouldBeNull = _context.breed.SingleOrDefault( b => b.Name == newPotentialPet.NewBreedName);
                    if(shouldBeNull != null)
                    {
                        //Logic for this did not seem to work as a custom validation, //TODO//
                        ViewBag.Error = "There is already a breed by that name";
                        return View("Dashboard");
                    }
                    Breed newBreed = new Breed{
                        Name = newPotentialPet.NewBreedName
                    };
                    _context.Add(newBreed);
                    _context.SaveChanges();
                    int newBreedId = _context.breed.Single( b => b.Name == newPotentialPet.NewBreedName).Id;
                    newPotentialPet.BreedId = newBreedId;
                }
                Pet newPet = new Pet{//Transfer to DB Model
                    Name = newPotentialPet.PetName,
                    BreedId = newPotentialPet.BreedId,
                    PetOwnerId = (int)activeId,
                    DateOfBirth = newPotentialPet.DateOfBirth,
                    Active = true
                };
                _context.Add(newPet);
                _context.SaveChanges();
                return RedirectToAction("Dashboard", "PetOwner");
            }
            return View("Dashboard");
        }
        [HttpGet]
        [Route("{change}Pet/{id}")]
        public IActionResult CancelPetPolicy(string change, int id)
        {
            int? activeId = HttpContext.Session.GetInt32("activeUser");
            bool isValid = SecurityCheck.CheckActiveUserVsPet(activeId, id, _context);
            if(isValid == false)
            {
                //Redirected to Logout as user may be malicious
                return RedirectToAction("Logout", "PetOwner");
            }
            var changingPet = _context.pet.SingleOrDefault( p => p.Id == id);
            //Switch case allows one route/method to handle different yet similar logic as needed
            switch(change)
            {
                case "Transfer":
                    return RedirectToAction("Transfer", new{ id = id });
                case "Cancel":
                    changingPet.Active = false;
                    break;
                case "Activate":
                    changingPet.Active = true;
                    break;
            }
            _context.SaveChanges();
            return RedirectToAction("Dashboard", "PetOwner");
        }
        [HttpGet]
        [Route("Pet/Transfer/{id}")]
        public IActionResult Transfer(int id)
        {
            int? activeId = HttpContext.Session.GetInt32("activeUser");
            bool isValid = SecurityCheck.CheckActiveUserVsPet(activeId, id, _context);
            if(isValid == false)
            {
                //Redirected to Logout as user may be malicious
                return RedirectToAction("Logout", "PetOwner");
            }
            var transferPet = _context.pet.SingleOrDefault( p => p.Id == id);
            TransferOwner transfer = new TransferOwner{
                PetToBeTransferred = (Pet)transferPet,
                CurrentOwner = _context.petowner.SingleOrDefault( o => o.Id == (int)activeId)
            };
            return View(transfer);
        }
        [HttpPost]
        [Route("Transfer/{petId}/From/{ownerId}")]
        public IActionResult ProcessTransfer(TransferOwner transfer, int petId, int ownerId)
        {
            int? activeId = HttpContext.Session.GetInt32("activeUser");
            bool isValid = SecurityCheck.CheckUserCanTransfer(activeId, petId, ownerId, _context);
            if(!isValid)
            {
                //Redirected to Logout as user may be malicious
                return RedirectToAction("Logout", "PetOwner");
            }
            var transferPet = _context.pet.SingleOrDefault( p => p.Id == petId);
            if(ModelState.IsValid)
            {
                int newOwnerId = _context.petowner.Single( o => o.Email == transfer.Email ).Id;
                transferPet.PetOwnerId = newOwnerId;
                transferPet.Active = false;
                _context.SaveChanges();
                return RedirectToAction("Dashboard", "PetOwner");
            }
            transfer.PetToBeTransferred = (Pet)transferPet;
            transfer.CurrentOwner = _context.petowner.SingleOrDefault( o => o.Id == (int)activeId);
            transfer.Email = "";
            return View("Transfer",transfer);
        }
    }
}