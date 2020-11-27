using AutoMapper;
using CmentarzKomunalny.Web.Data.Interfaces;
using CmentarzKomunalny.Web.DTOs;
using CmentarzKomunalny.Web.Models.CommandTestAPI;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CmentarzKomunalny.Web.Controllers
{
    //api/commands having a route, it may change the route depending on the class name or something
    [Route("api/[controller]")] // or defined bezposrednio (XD) api/controllername or api/[controller]
    [ApiController]
    public class CommandController : ControllerBase // we dont have views for now
    {
        private readonly ICommandRepo _repository; // dependency injection method
        private readonly IMapper _mapper;

        public CommandController(ICommandRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //GET api/command - getting all commands
        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            return Ok(commandItems); // 200 success in postman
        }

        //GET api/command/{id} - getting command by its id
        public ActionResult<CommandReadDto> GetCommandById(int id) // we're using Dto because we dont want others to see the stuff we're doing, kinda
        {
            var commandItem = _repository.GetCommandById(id);
            if(commandItem != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(commandItem)); // 200 success / we're mapping by CommandReadDto, and the data comes from commandItem
            }
            return NotFound();
            
        }


    }
}
