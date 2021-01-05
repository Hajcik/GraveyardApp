using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AutoMapper;
using CmentarzKomunalny.Web.Data.Interfaces;
using CmentarzKomunalny.Web.DTOs.LodgingDtos;
using CmentarzKomunalny.Web.Models.Cmentarz;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CmentarzKomunalny.Web.Controllers
{
    // api/lodging
    [Route("api/[controller]")] // to tez trzeba z wyszukiwarka grobow polaczyc
    [ApiController]
    public class LodgingController : ControllerBase
    {
        private readonly ILodgingsRepo _repository; // dependency injection
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public LodgingController(ILodgingsRepo repository, IMapper mapper, IConfiguration configuration)
        {
            _configuration = configuration;
            _repository = repository;
            _mapper = mapper;
        }
        // get all lodgings
        //GET api/lodging
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select IdLodge, LodgingType, isMonument, PeopleLimit, isReserved from dbo.Lodgings";
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

        // SEARCHING LODGING BY ITS ID
        //GET api/lodging/LodgeById
        [HttpGet("LodgeById/{id}")]
        public ActionResult<LodgingReadDto> GetLodgeById(int id)
        {
            var lodgeId = _repository.GetLodgeById(id);
            if (lodgeId != null)
                return Ok(_mapper.Map<LodgingReadDto>(lodgeId));
            return NotFound();
        }

        [HttpPost]
        public JsonResult Post(Lodging lodge)
        {

            string query = @"
                insert into dbo.Lodgings values
                ('" + Convert.ToInt32(lodge.LodgingType) + @"', N'" + lodge.isMonument + @"',
                 N'" + lodge.PeopleLimit + @"', '" + lodge.isReserved + @"')";

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
                return new JsonResult("Dodano kwaterę pomyślnie");
            }
        }

        [HttpPut]
        public JsonResult Put(Lodging lodge)
        {
            string query = @"
                update dbo.DeadPeople set
                LodgingType = '" + lodge.LodgingType + @"',
                isMonument = '" + lodge.isMonument + @"',
                PeopleLimit = '" + lodge.PeopleLimit + @"',
                isReserved = '" + lodge.isReserved + @"'
                where IdLodge = " + lodge.IdLodge + @"";

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
                return new JsonResult("Zaktualizowano kwaterę pomyślnie");
            }
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                delete from dbo.Lodgings
                where IdLodge = " + id + @"";

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
                return new JsonResult("Kwatera usunięta pomyślnie");
            }
        }

        // PATCH
        //PATCH api/lodging/LodgeById
        [HttpPatch("LodgeById/{id}")]
        public ActionResult PartialLodgeUpdate(int id, JsonPatchDocument<LodgingAddDto> patchDoc)
        {
            var lodgeFromRepo = _repository.GetLodgeById(id);
            if (lodgeFromRepo == null)
                return NotFound();

            var lodgeToPatch = _mapper.Map<LodgingAddDto>(lodgeFromRepo);
            patchDoc.ApplyTo(lodgeToPatch, ModelState);

            _mapper.Map(lodgeToPatch, lodgeFromRepo);
            _repository.UpdateLodge(lodgeFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

    }
}

/*
 * public ActionResult<IEnumerable<LodgingReadDto>> GetAllLodgings()
        {
            var lodgings = _repository.GetAllLodgings();
            return Ok(_mapper.Map<IEnumerable<LodgingReadDto>>(lodgings));
        }
// add new lodge
        //POST api/lodging/
        [HttpPost]
        public ActionResult<LodgingAddDto> AddLodgeToDb(LodgingAddDto lodgeAddDto)
        {
            
            var lodgeModel = _mapper.Map<Lodging>(lodgeAddDto);
            _repository.AddLodgeToDb(lodgeModel);
            _repository.SaveChanges();
            var lodgeReadDto = _mapper.Map<LodgingReadDto>(lodgeModel);
            return CreatedAtRoute(nameof(GetLodgeById), new { Id = lodgeReadDto.IdLodge }, lodgeReadDto);
        }
// UPDATE LODGE CONTENT BY ITS ID
        //PUT api/lodging/LodgeById
        [HttpPut("LodgeById/{id}")]
        public ActionResult UpdateLodge(int id, LodgingAddDto lodgingAddDto)
        {
            var lodgeFromRepo = _repository.GetLodgeById(id);
            if (lodgeFromRepo == null)
                return NotFound();
            _mapper.Map(lodgingAddDto, lodgeFromRepo);
            _repository.UpdateLodge(lodgeFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
//DELETE api/lodging/LodgeById
        [HttpDelete("LodgeById/{id}")]
        public ActionResult DeleteLodgeFromDb(int id)
        {
            var lodgeFromRepo = _repository.GetLodgeById(id);
            if (lodgeFromRepo == null)
                return NotFound();
            _repository.DeleteLodgeFromDb(lodgeFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
*/