using CmentarzKomunalny.Web.Models.CommandTestAPI;
using System.Collections.Generic;

namespace CmentarzKomunalny.Web.Data.Interfaces
{
    public interface ICommandRepo
    {
        bool SaveChanges();

        IEnumerable<Command> GetAllCommands();
        Command GetCommandById(int id);
        void CreateCommand(Command cmd);
        void UpdateCommand(Command cmd);
        void DeleteCommand(Command cmd);

    }
}
