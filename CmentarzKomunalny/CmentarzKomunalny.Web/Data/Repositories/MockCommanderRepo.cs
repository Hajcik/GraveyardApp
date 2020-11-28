using CmentarzKomunalny.Web.Data.Interfaces;
using CmentarzKomunalny.Web.Models.CommandTestAPI;
using System.Collections.Generic;

//testing repo

namespace CmentarzKomunalny.Web.Data.Repositories
{
    public class MockCommanderRepo : ICommandRepo
    {
        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                new Command{Id = 0, HowTo="blabla", Line="testbla", Platform="bla and bla" },
                new Command{Id = 1, HowTo="lala", Line="testla", Platform="la and la"}
            };

            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command
            {
                Id = 0,
                HowTo = "blabla",
                Line = "testbla",
                Platform = "bla and bla"
            };
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}
