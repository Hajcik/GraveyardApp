using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CmentarzKomunalny.Web.Data.Interfaces;
using CmentarzKomunalny.Web.Data.Repositories;
using CmentarzKomunalny.Web.DTOs.DeadPersonDtos;
using CmentarzKomunalny.Web.Models.Cmentarz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
namespace CmentarzKomunalny.Web.Controllers
{   // api/deadperson
    [Route("api/[controller]")] // tutaj chyba nazwa "wyszukiwarka-grobow"
    [ApiController]
    // currently without views
    [Authorize(Policy = "RequireAdministratorRole")]
    [Authorize(Policy = "RequireEmployeeRole")]
    public class DeadPersonController : ControllerBase
    {
        private readonly MockDeadPeopleRepo _mockRepo = new MockDeadPeopleRepo();
        private readonly IConfiguration _configuration;
        private readonly IDeadPeopleRepo _repository; // dependency injection
        private readonly IMapper _mapper;

        public DeadPersonController(IConfiguration configuration, IDeadPeopleRepo repository, IMapper mapper)
        {
            _configuration = configuration;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select LodgingId, Name, DateOfBirth, DateOfDeath from dbo.DeadPeople";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CmentarzConnectionTEST");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
                return new JsonResult(table);
            }
        }

        [HttpPost]
        public JsonResult Post(DeadPerson deadp)
        {
            string query = @"
                insert into dbo.DeadPeople values
                (N'" + deadp.Name + @"', N'" + deadp.DateOfBirth + @"',
                 N'" + deadp.DateOfDeath + @"', '" + deadp.LodgingId + @"')";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CmentarzConnectionTEST");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
                return new JsonResult("Dodano pomyślnie");
            }
        }

        // update
        [HttpPut]
        public JsonResult Put(DeadPerson deadp)
        {
            string query = @"
                update dbo.DeadPeople set
                Name = N'" + deadp.Name + @"',
                DateOfBirth = '" + deadp.DateOfBirth + @"',
                DateOfDeath = '" + deadp.DateOfDeath + @"',
                LodgingId = '" + deadp.LodgingId + @"'
                where IdDeadPerson = " + deadp.IdDeadPerson + @"";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CmentarzConnectionTEST");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
                return new JsonResult("Zaktualizowano pomyślnie");
            }
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                delete from dbo.DeadPeople
                where IdDeadPerson = " + id + @"";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CmentarzConnectionTEST");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
                return new JsonResult("Zaktualizowano pomyślnie");
            }
        }

        // SEARCHING DEAD PERSON BY ITS ID
        //GET api/deadperson
        [HttpGet("DeadPersonById/{id}", Name = "GetDeadPersonById")]
        public ActionResult<DeadPersonReadDto> GetDeadPersonById(int id)
        {
            var deadPersonId = _repository.GetDeadPersonById(id);
            if (deadPersonId != null)
                return Ok(_mapper.Map<DeadPersonReadDto>(deadPersonId));
            return NotFound();
        }

        // SEARCHING DEAD PEOPLE BY ITS LODGE ID
        //GET api/deadperson/{id} - lodge id

        [HttpGet("DeadPeopleByLodgeId/{id}", Name = "GetDeadPersonByLodgeId")]
        public ActionResult<DeadPersonReadDto> GetDeadPeopleByLodgeId(int id)
        {
            var deadPeopleLodgeId = _repository.GetDeadPeopleByLodgeId(id);
            if (deadPeopleLodgeId != null)
                return Ok(_mapper.Map<IEnumerable<DeadPersonReadDto>>(deadPeopleLodgeId));
            return NotFound();
        }

        // PATCH
        //PATCH api/deadPersonById/{id}
        [HttpPatch("DeadPersonById/{id}")]
        public ActionResult PartialDeadPersonUpdate(int id, JsonPatchDocument<DeadPersonAddDto> patchDoc)
        {
            var deadPersonFromRepo = _repository.GetDeadPersonById(id);
            if (deadPersonFromRepo == null)
                return NotFound();

            var deadPersonToPatch = _mapper.Map<DeadPersonAddDto>(deadPersonFromRepo);
            patchDoc.ApplyTo(deadPersonToPatch, ModelState);

            if (!TryValidateModel(deadPersonToPatch))
                return ValidationProblem(ModelState);

            _mapper.Map(deadPersonToPatch, deadPersonFromRepo);

            _repository.UpdateDeadPerson(deadPersonFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

    }
}

// ADDING DEAD PERSON TO DATABASE
//POST api/deadperson
/*     [HttpPost]
     public ActionResult<DeadPersonAddDto> AddDeadPersonToDb(DeadPersonAddDto deadPersonAddDto)
     {
         var deadPersonModel = _mapper.Map<DeadPerson>(deadPersonAddDto);
         _repository.AddDeadPersonToDb(deadPersonModel);
         _repository.SaveChanges();
         var deadPersonReadDto = _mapper.Map<DeadPersonReadDto>(deadPersonModel);
         return CreatedAtRoute(nameof(GetDeadPersonById), new { Id = deadPersonReadDto.IdDeadPerson }, deadPersonReadDto);
     }
*/
// UPDATE DEAD PERSON CONTENT BY ITS DEAD PERSON ID
//PUT api/deadperson/DeadPersonById/{id}
/*     [HttpPut("DeadPersonById/{id}")]
     public ActionResult UpdateDeadPerson(int id, DeadPersonAddDto deadPersonAddDto)
     {
         var deadPersonFromRepo = _repository.GetDeadPersonById(id);
         if (deadPersonFromRepo == null)
             return NotFound();
         _mapper.Map(deadPersonAddDto, deadPersonFromRepo);
         _repository.UpdateDeadPerson(deadPersonFromRepo);
         _repository.SaveChanges();
         return NoContent();
     }
*/

// DELETE DEAD PERSON FROM DATABASE BY ITS ID
//DELETE api/deadperson/DeadPersonById/{id}
/*      [HttpDelete("DeadPersonById/{id}")]
      public ActionResult DeleteDeadPersonFromDb(int id)
      {
          var deadPersonFromRepo = _repository.GetDeadPersonById(id);
          if (deadPersonFromRepo == null)
              return NotFound();
          _repository.DeleteDeadPersonFromDb(deadPersonFromRepo);
          _repository.SaveChanges();
          return NoContent();
      }
  }
*/