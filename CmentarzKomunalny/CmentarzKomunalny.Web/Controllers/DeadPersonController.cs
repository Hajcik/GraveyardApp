using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CmentarzKomunalny.Web.Data.Interfaces;
using CmentarzKomunalny.Web.Data.Repositories;
using CmentarzKomunalny.Web.Models.Cmentarz;
using Microsoft.AspNetCore.Mvc;

namespace CmentarzKomunalny.Web.Controllers
{   // api/deadperson
    [Route("api/[controller]")]
    [ApiController]
    public class DeadPersonController : ControllerBase
    {
        private readonly MockDeadPeopleRepo _mockRepo = new MockDeadPeopleRepo();
        private readonly IDeadPeopleRepo _repository; // dependency injection
        private readonly IMapper _mapper;

        public DeadPersonController(IDeadPeopleRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        //GET api/deadperson
        [HttpGet]
        public ActionResult <DeadPerson> GetAllDeadPeople()
        {
            var deadPeople = _mockRepo.GetAllDeadPeople();
            return Ok(deadPeople);
        }
    }
}
