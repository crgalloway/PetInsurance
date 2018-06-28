using System.Linq;
using ProblemD.Models;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ProblemD.Models
{
    public class SecurityCheck
    {
        public static bool CheckForActiveUser( int? activeId )
        {
            if(activeId == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool CheckIfUserAuthorized ( int? activeId, int ownerId)
        {
            if(activeId == null || ownerId != (int)activeId)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool CheckActiveUserVsPet ( int? activeId, int petId, ProblemDContext _context )
        {
            var test = _context.petowner.ToList();
            Pet transferPet = _context.pet.SingleOrDefault( p => p.Id == petId);
            if(activeId == null || transferPet == null || transferPet.PetOwnerId != (int)activeId)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool CheckUserCanTransfer (int? activeId, int petId, int ownerId, ProblemDContext _context  )
        {
            var transferPet = _context.pet.SingleOrDefault( p => p.Id == petId);
            if(activeId == null || transferPet == null || transferPet.PetOwnerId != (int)activeId || ownerId != (int)activeId)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}