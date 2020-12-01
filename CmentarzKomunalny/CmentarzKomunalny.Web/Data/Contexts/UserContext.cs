using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CmentarzKomunalny.Web.Models.Cmentarz;

namespace CmentarzKomunalny.Web.Data.Contexts
{
    public class UserContext : IdentityDbContext<User, Role, int>
    {
        public virtual DbSet<Employee> Employees { get; set; }
    }
}
