using System;
using System.Collections.Generic;

namespace ProblemD.Models
{
    public class Pet : BaseEntity
    {
        public DateTime DateOfBirth {get;set;}
        public int PetOwnerId {get;set;}
        public PetOwner Owner {get;set;}
        public int BreedId {get;set;}
        public Breed Breed {get;set;}
    }
}