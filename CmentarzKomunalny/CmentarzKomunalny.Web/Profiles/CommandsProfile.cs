using AutoMapper;
using CmentarzKomunalny.Web.Models.CommandTestAPI;
using CmentarzKomunalny.Web.DTOs;

namespace CmentarzKomunalny.Web.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            CreateMap<Command, CommandReadDto>();
        }
    }
}
