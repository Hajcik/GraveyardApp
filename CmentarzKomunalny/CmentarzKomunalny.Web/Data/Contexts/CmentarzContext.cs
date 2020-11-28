using Microsoft.EntityFrameworkCore;
using CmentarzKomunalny.Web.Models.Cmentarz;
namespace CmentarzKomunalny.Web.Data.Contexts
{
    public class CmentarzContext : DbContext
    {
        public CmentarzContext(DbContextOptions<CmentarzContext> opt) : base(opt)
        {

        }

        public DbSet<Employee> Employees { get; set; } // we need to do the mapping
        // other models to do
    }
}
