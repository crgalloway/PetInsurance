using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProblemD.Models
{
    public class PetValidation
    {
        [Required(ErrorMessage="Name required")]
        [MinLength(2, ErrorMessage="Name must be 2 characters or longer")]
        [Display(Name="Name: ")]
        public string Name {get;set;}
        [InThePast]
        public DateTime DateOfBirth {get;set;}
        public int BreedId {get;set;}
    }
}