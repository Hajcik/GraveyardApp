using Microsoft.EntityFrameworkCore;
using CmentarzKomunalny.Web.Models.Cmentarz;
namespace CmentarzKomunalny.Web.Data.Contexts
{
    public class CmentarzContext : DbContext
    {
        public CmentarzContext(DbContextOptions<CmentarzContext> opt) : base(opt)
        {

        }

 //       public virtual DbSet<Employee> Employees { get; set; } // we need to do the mapping
        public DbSet<DeadPerson> DeadPeople { get; set; }
        public DbSet<Lodging> Lodgings { get; set; }
        public DbSet<Obituary> Obituaries { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Graveyard> GraveyardLimits { get; set; }
      
        // other models to do

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
