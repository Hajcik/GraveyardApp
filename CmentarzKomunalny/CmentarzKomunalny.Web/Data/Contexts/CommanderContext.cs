using CmentarzKomunalny.Web.Models.CommandTestAPI;
using Microsoft.EntityFrameworkCore;


namespace CmentarzKomunalny.Web.Data.Contexts
{
    public class CommanderContext : DbContext
    {
        public CommanderContext(DbContextOptions<CommanderContext> opt) : base(opt)
        { // options

        }

        public DbSet<Command> Commands { get; set; } // we need to do the mapping
        // other models ...
    }
}
