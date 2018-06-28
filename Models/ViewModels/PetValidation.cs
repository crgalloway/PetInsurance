using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProblemD.Models
{
    public class PetValidation
    {
        [Required(ErrorMessage="Name required")]
        [MinLength(2, ErrorMessage="Name must be 2 characters or longer")]
        [Display(Name="Pet's Name: ")]
        public string PetName {get;set;}
        [InThePast]
        [Display(Name="Pet's Date of Birth: ")]
        public DateTime DateOfBirth {get;set;} = DateTime.Now;
        [Display(Name="Select Breed: ")]
        public int BreedId {get;set;}
        [MinLengthIfAny]
        public string NewBreedName {get;set;}
        public List<Breed> AllBreeds {get;set;}
        public PetValidation()
        {
            AllBreeds = new List<Breed>();
        }
    }
}