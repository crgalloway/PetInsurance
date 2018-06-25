using System.ComponentModel.DataAnnotations;
using System;

namespace ProblemD.Models
{
    public class InThePastAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime now = DateTime.Now;
            var timeDiff = DateTime.Compare((DateTime)value, now);
            if(timeDiff>0)
            {
                return new ValidationResult("Date must be in the past");
            }
            return ValidationResult.Success;
        }
    }
}