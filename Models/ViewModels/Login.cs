using System.ComponentModel.DataAnnotations;

namespace ProblemD.Models
{
    public class Login
    {
        [EmailAddress]
        [Display(Name="Email: ")]
        public string LoginEmail{get;set;}
        [Display(Name="Password: ")]
        [ValidLogin]
        public string LoginPassword{get;set;}
    }
}