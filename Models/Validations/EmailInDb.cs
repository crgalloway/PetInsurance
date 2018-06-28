using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace ProblemD.Models
{
    public class EmailInDbAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ProblemDContext _context = (ProblemDContext) validationContext.GetService(typeof(ProblemDContext));
            var petOwner = _context.petowner.SingleOrDefault( o => o.Email == (string)value);
            if(petOwner == null)
            {
                return new ValidationResult("There is no Pet Owner with that email address");
            }
            return ValidationResult.Success;
        }
    }
}