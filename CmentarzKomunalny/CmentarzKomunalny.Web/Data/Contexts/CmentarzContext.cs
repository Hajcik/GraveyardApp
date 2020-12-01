using Microsoft.EntityFrameworkCore;
using CmentarzKomunalny.Web.Models.Cmentarz;
namespace CmentarzKomunalny.Web.Data.Contexts
{
    public class CmentarzContext : DbContext
    {
        public CmentarzContext(DbContextOptions<CmentarzContext> opt) : base(opt)
        {

        }

        public virtual DbSet<Employee> Employees { get; set; } // we need to do the mapping
        public virtual DbSet<DeadPerson> DeadPeople { get; set; }
        // other models to do

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // fluent api commands
            modelBuilder.Entity<User>()
                .ToTable("AspNetUsers")
                .HasDiscriminator<int>("UserType")
                .HasValue<User>((int)RoleValue.User)
                .HasValue<Employee>((int)RoleValue.Employee)
                .HasValue<Administrator>((int)RoleValue.Admin);
        }
    }
}
