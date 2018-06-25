using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Microsoft.AspNetCore.Identity;

namespace ProblemD.Models
{
    public class ValidLoginAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ProblemDContext _context = (ProblemDContext) validationContext.GetService(typeof(ProblemDContext));

            Object instance = validationContext.ObjectInstance;
            Type type = instance.GetType();
            string email = (string)type.GetProperty("Email ").GetValue(instance, null);
            var potentialOwner = _context.petowner.SingleOrDefault( o => o.Email == email );
            PasswordHasher<PetOwner> hasher = new PasswordHasher<PetOwner>();
            if(potentialOwner != null && hasher.VerifyHashedPassword(potentialOwner, potentialOwner.Password, (string)value) != 0)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Invalid login attempt");
            
        }
    }
}