using CmentarzKomunalny.Web.Data.Contexts;
using CmentarzKomunalny.Web.Data.Interfaces;
using CmentarzKomunalny.Web.Models.CommandTestAPI;
using System;
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

        public void CreateCommand(Command cmd)
        {
            if (cmd == null)
                throw new ArgumentNullException(nameof(cmd));

            _context.Commands.Add(cmd);
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return _context.Commands.ToList();
        }

        public Command GetCommandById(int id)
        {
            return _context.Commands.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
            // whenever you make a change to dbcontext, the data would be changed on database
            // unless we call this method, we're gonna use create/add but until we call "SaveChanges"
            // that won't be implemented in the database!
        }

        public void UpdateCommand(Command cmd)
        {
            // Nothing, kinda counterintuitive
        }

        public void DeleteCommand(Command cmd)
        {
            if (cmd == null)
                throw new ArgumentNullException(nameof(cmd));

            _context.Commands.Remove(cmd);
            // we'll need to run .SaveChanges in controller for it to work
        }
    }
}
