using System.Collections.Generic;
using AutoMapper;
using CmentarzKomunalny.Web.Data.Interfaces;
using CmentarzKomunalny.Web.DTOs.LodgingDtos;
using CmentarzKomunalny.Web.Models.Cmentarz;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CmentarzKomunalny.Web.Controllers
{
    // api/lodging
    [Route("api/[controller]")] // to tez trzeba z wyszukiwarka grobow polaczyc
    [ApiController]
    public class LodgingController : ControllerBase
    {
        private readonly ILodgingsRepo _repository; // dependency injection
        private readonly IMapper _mapper;
        public LodgingController(ILodgingsRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        // get all lodgings
        //GET api/lodging
        [HttpGet]
        public ActionResult<IEnumerable<LodgingReadDto>> GetAllLodgings()
        {
            var lodgings = _repository.GetAllLodgings();
            return Ok(_mapper.Map<IEnumerable<LodgingReadDto>>(lodgings));
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
    }
}
