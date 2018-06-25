using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProblemD.Models
{
    public class PetOwnerValidation
    {
        [Required(ErrorMessage="Name required")]
        [MinLength(2, ErrorMessage="Name must be 2 characters or longer")]
        [Display(Name="Name: ")]
        public string Name {get;set;}
        [EmailAddress]
        [Required]
        [Display(Name="Email: ")]
        [Unique]
        public string Email {get;set;}
        [Required(ErrorMessage="Password required")]
        [MinLength(8, ErrorMessage="Password must be 8 characters or longer")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$", ErrorMessage="Password must contain at least 1 number, 1 letter, and a special character")]
        [Display(Name="Password: ")]
        public string Password {get;set;}
        [Compare("password", ErrorMessage="Password Confirmation must match")]
        [Display(Name="Confirm Password: ")]
        public string PasswordConfirmation {get;set;}
        [Required]
        public int CountryId {get;set;}
    }
}