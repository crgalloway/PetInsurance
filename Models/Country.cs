using System.Collections.Generic;

namespace ProblemD.Models
{
    public class Country : BaseEntity
    {
        public string IsoCode {get;set;}
        public List<PetOwner> Residents {get;set;}
        public Country()
        {
            Residents = new List<PetOwner>();
        }
    }
}