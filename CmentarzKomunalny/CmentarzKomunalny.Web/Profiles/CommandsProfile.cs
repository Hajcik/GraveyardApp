using AutoMapper;
using CmentarzKomunalny.Web.Models.CommandTestAPI;
using CmentarzKomunalny.Web.DTOs;

namespace CmentarzKomunalny.Web.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {   // Source -> Target destination
            // ^Command -> ^ReadDto // kind of self-explanatory
            CreateMap<Command, CommandReadDto>();

            // we're mapping it back to command
            CreateMap<CommandCreateDto, Command>();
            // making a translation from one way to another you have to explicitly declare it

            CreateMap<CommandUpdateDto, Command>();
            // source is update, and want to apply that to command object

            // for patch 
            CreateMap<Command, CommandUpdateDto>();
        }
    }
}
