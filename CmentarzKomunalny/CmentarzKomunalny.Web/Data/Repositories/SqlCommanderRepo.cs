using CmentarzKomunalny.Web.Data.Contexts;
using CmentarzKomunalny.Web.Data.Interfaces;
using CmentarzKomunalny.Web.Models.CommandTestAPI;
using System.Collections.Generic;
using System.Linq;

namespace CmentarzKomunalny.Web.Data.Repositories
{
    public class SqlCommanderRepo : ICommandRepo
    {
        private readonly CommanderContext _context;
        public SqlCommanderRepo(CommanderContext context)
        {
            _context = context;
        }
        public IEnumerable<Command> GetAllCommands()
        {
            return _context.Commands.ToList();
        }

        public Command GetCommandById(int id)
        {
            return _context.Commands.FirstOrDefault(p => p.Id == id);
        }
    }
}
