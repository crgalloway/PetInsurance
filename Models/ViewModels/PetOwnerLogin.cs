using System.ComponentModel.DataAnnotations;

namespace ProblemD.Models
{
    public class PetOwnerLogin
    {
        [EmailAddress]
        [Display(Name="Email: ")]
        public string LoginEmail {get;set;}
        [Display(Name="Password: ")]
        [ValidLogin]
        public string LoginPassword {get;set;}
    }
}