using AutoMapper;
using CmentarzKomunalny.Web.Data.Interfaces;
using CmentarzKomunalny.Web.DTOs;
using CmentarzKomunalny.Web.Models.CommandTestAPI;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CmentarzKomunalny.Web.Controllers
{
    //api/commands having a route, it may change the route depending on the class name or something
    [Route("api/[controller]")] // or defined bezposrednio (XD) api/controllername or api/[controller]
    [ApiController]
    public class CommandsController : ControllerBase // we dont have views for now
    {
        private readonly ICommandRepo _repository; // dependency injection method
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //GET api/commands - getting all commands
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems)); // 200 success in postman
        }

        //GET api/commands/{id} - getting command by its id
        // we gave it a NAME to get use of it in other parts of this code
        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id) // we're using Dto because we dont want others to see the stuff we're doing, kinda
        {
            var commandItem = _repository.GetCommandById(id);
            if (commandItem != null)
            {
                // return Ok(commandItem);
                return Ok(_mapper.Map<CommandReadDto>(commandItem)); // 200 success / we're mapping by CommandReadDto, and the data comes from commandItem
            }
            return NotFound();

        }

        //POST api/commands
        [HttpPost]
        public ActionResult<CommandCreateDto> CreateCommand(CommandCreateDto commandCreateDto) // not Command cmd? worth noting
        {   // we want to map to the object (Command), and the source is commandCreateDto
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto); // 201 Created, it gives full url to the file
                                                                                                           // to check this stuff checkout docs.microsoft.net about ApiController.CreatedAtRoute
                                                                                                           //    return Ok(commandReadDto); // not final
        }

        //PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {   // kinda placeholder
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
                return NotFound();

            _mapper.Map(commandUpdateDto, commandModelFromRepo);
            // this mapping actually updates our model and those changes are being tracked
            // by DbContext, so we dont need to do anything in UpdateCommand in SqlRepo
            // but good practice is still to call Update method on our repository and supply our model
            // our implementations may need that

            _repository.UpdateCommand(commandModelFromRepo);

            _repository.SaveChanges(); // we need to do that!

            return NoContent();
        }


        //PATCH api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
                return NotFound();

            // we check if we have resource to update to our repo

            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
            patchDoc.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch))
                return ValidationProblem(ModelState);

            _mapper.Map(commandToPatch, commandModelFromRepo);
            _repository.UpdateCommand(commandModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
                return NotFound();

            _repository.DeleteCommand(commandModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
