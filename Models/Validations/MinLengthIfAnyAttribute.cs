using System.ComponentModel.DataAnnotations;
using System;

namespace ProblemD.Models
{
    //Checks to see if anything is entered, and if there is that it is at least 3 characters long
    public class MinLengthIfAnyAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string stringValue = (string) value;
            if(stringValue.Length == 0 || stringValue.Length >2)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Breed name must be longer than 2 characters");
        }
    }
}