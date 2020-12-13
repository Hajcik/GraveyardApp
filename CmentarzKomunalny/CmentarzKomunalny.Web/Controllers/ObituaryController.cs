using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CmentarzKomunalny.Web.DTOs.NewsDtos;
using CmentarzKomunalny.Web.Data.Interfaces;
using CmentarzKomunalny.Web.Models.Cmentarz;
using Microsoft.AspNetCore.JsonPatch;
using CmentarzKomunalny.Web.DTOs.ObituaryDtos;

namespace CmentarzKomunalny.Web.Controllers
{
    // api/obituary
    [Route("api/[controller]")] // polaczyc z 'nekrologi'
    [ApiController]
    public class ObituaryController : ControllerBase
    {
        private readonly IObituaryRepo _repository;
        private readonly IMapper _mapper;
        public ObituaryController(IObituaryRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ObituaryReadDto>> GetAllObituaries()
        {
            var obituaries = _repository.GetAllObituaries();
            return Ok(_mapper.Map<IEnumerable<ObituaryReadDto>>(obituaries));
        }

        // search obituary by its ID
        //GET api/obituary
        [HttpGet("{id}", Name = "GetObituaryById")]
        public ActionResult<ObituaryReadDto> GetObituaryById(int id)
        {
            var obituaryId = _repository.GetObituaryById(id);
            if (obituaryId != null)
                return Ok(_mapper.Map<ObituaryReadDto>(obituaryId));
            return NotFound();
        }
        // add new obituary
        //POST api/obituary
        [HttpPost]
        public ActionResult<ObituaryAddDto> AddObituary(ObituaryAddDto obituaryAddDto)
        {
            var obituaryModel = _mapper.Map<Obituary>(obituaryAddDto);
            var obituaryReadDto = _mapper.Map<NewsReadDto>(obituaryModel);
            _repository.AddObituary(obituaryModel);
            _repository.SaveChanges();

            return CreatedAtRoute(nameof(GetObituaryById), new { Id = obituaryReadDto.Id }, obituaryReadDto);
        }

        // update obituary content by its id
        //PUT api/obituary
        [HttpPut("{id}")]
        public ActionResult UpdateObituary(int id, ObituaryAddDto obituaryAddDto)
        {
            var obituaryFromRepo = _repository.GetObituaryById(id);
            if (obituaryFromRepo == null)
                return NotFound();

            _mapper.Map(obituaryAddDto, obituaryFromRepo);
            _repository.UpdateObituary(obituaryFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        // PATCH
        //PATCH api/obituary
        [HttpPatch("{id}")]
        public ActionResult PartialObituaryUpdate(int id, JsonPatchDocument<ObituaryAddDto> patchDoc)
        {
            var obituaryFromRepo = _repository.GetObituaryById(id);
            if (obituaryFromRepo == null)
                return NotFound();

            var obituaryToPatch = _mapper.Map<ObituaryAddDto>(obituaryFromRepo);
            patchDoc.ApplyTo(obituaryToPatch, ModelState);

            _mapper.Map(obituaryToPatch, obituaryFromRepo);
            _repository.UpdateObituary(obituaryFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        //DELETE api/obituary
        [HttpDelete("{id}")]
        public ActionResult DeleteObituary(int id)
        {
            var obituaryFromRepo = _repository.GetObituaryById(id);
            if (obituaryFromRepo == null)
                return NotFound();

            _repository.DeleteObituary(obituaryFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
