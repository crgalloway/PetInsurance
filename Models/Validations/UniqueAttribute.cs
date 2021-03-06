using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace ProblemD.Models
{
    //Currently only set to check if the email is Unique, could be adjusted to meet specific needs
    public class UniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ProblemDContext _context = (ProblemDContext) validationContext.GetService(typeof(ProblemDContext));
            var matchingEmail = _context.petowner.SingleOrDefault( o => o.Email == (string)value );
            if(matchingEmail != null)
            {
                return new ValidationResult("Email already exists in database");
            }
            return ValidationResult.Success;
        }
    }
}