using System;
using System.Collections.Generic;

namespace ProblemD.Models
{
    public class PetOwner : BaseEntity
    {
        public string Email {get;set;}
        public string Password {get;set;}
        public string PolicyNumber {get;set;}
        public DateTime EnrollmentDate {get;set;}
        public int CountryId {get;set;}
        public Country CountryOfResidence {get;set;}
        public bool Active {get;set;}
        public List<Pet> OwnedPets {get;set;}
        public PetOwner()
        {
            OwnedPets = new List<Pet>();
        }
    }
}