using Microsoft.EntityFrameworkCore;

namespace ProblemD.Models
{
    public class ProblemDContext : DbContext
    {
        public ProblemDContext(DbContextOptions<ProblemDContext> options) : base(options) {}
        public DbSet<PetOwner> petowner {get;set;}
        public DbSet<Country> country {get;set;}
        public DbSet<Pet> pet {get;set;}
        public DbSet<Breed> breed {get;set;}
    }
}