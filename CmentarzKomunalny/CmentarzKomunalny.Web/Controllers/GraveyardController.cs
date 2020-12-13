using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CmentarzKomunalny.Web.Data.Interfaces;
using CmentarzKomunalny.Web.DTOs.GraveyardDtos;

namespace CmentarzKomunalny.Web.Controllers
{
    [Route("api/graveyard")]
    [ApiController]
    public class GraveyardController : ControllerBase
    {
        private readonly IGraveyardRepo _repository;
        private readonly IMapper _mapper;
        public GraveyardController(IGraveyardRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // get all info
        [HttpGet]
        public ActionResult <IEnumerable<GraveyardReadDto>> GetGraveyardInfo()
        {
            var graveyard = _repository.GetGraveyardInfo();
            return Ok(_mapper.Map<IEnumerable<GraveyardReadDto>>(graveyard));
        }

        // do later

        // update info about graveyard
 //       [HttpPut]
 //       public ActionResult UpdateGraveyardDb(int id, GraveyardUpdateDto graveyardUpdateDto)
 //       {
 //           var graveyardRepo = _repository.
 //       }
        // patch info about graveyard
 //       [HttpPatch("{id}")]
       
    }
}
