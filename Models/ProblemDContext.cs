using Microsoft.EntityFrameworkCore;

namespace ProblemD.Models
{
    public class ProblemDContext : DbContext
    {
        public ProblemDContext(DbContextOptions<ProblemDContext> options) : base(options) {}
    }
}