using System;
using System.ComponentModel.DataAnnotations;

namespace ProblemD.Models
{
    public class TransferOwner
    {
        public PetOwner CurrentOwner {get;set;}
        public Pet PetToBeTransferred {get;set;}
        [EmailAddress]
        [EmailInDb]
        public string Email {get;set;}
    }
}