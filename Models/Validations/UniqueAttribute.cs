using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace ProblemD.Models
{
    public class UniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ProblemDContext _context = (ProblemDContext) validationContext.GetService(typeof(ProblemDContext));
            List<PetOwner> allOwners = _context.petowner.ToList();
            foreach(PetOwner each in allOwners)
            {
                if((string)value == (string)each.Email)
                {
                    return new ValidationResult("Email already exists in database");
                }
            }
            return ValidationResult.Success;
        }
    }
}