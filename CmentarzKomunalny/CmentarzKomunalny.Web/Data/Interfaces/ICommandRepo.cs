using CmentarzKomunalny.Web.Models.CommandTestAPI;
using System.Collections.Generic;

namespace CmentarzKomunalny.Web.Data.Interfaces
{
    public interface ICommandRepo
    {
        IEnumerable<Command> GetAllCommands();
        Command GetCommandById(int id);
    }
}
