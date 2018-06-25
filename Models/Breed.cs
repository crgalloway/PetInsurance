using System.Collections.Generic;

namespace ProblemD.Models
{
    public class Breed : BaseEntity
    {
        public List<Pet> Animals {get;set;}
        public Breed()
        {
            Animals = new List<Pet>();
        }
    }
}