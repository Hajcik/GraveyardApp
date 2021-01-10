using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Data.SqlClient;
using System.Data;
using CmentarzKomunalny.Web.DTOs.NewsDtos;
using CmentarzKomunalny.Web.Data.Interfaces;
using CmentarzKomunalny.Web.Models.Cmentarz;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace CmentarzKomunalny.Web.Controllers
{
    // api/news
    [Route("api/[controller]")] // polaczyc z 'aktualnosci'
    [ApiController]
    
    public class NewsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly INewsRepo _repository;
        private readonly IMapper _mapper;

        public NewsController(IConfiguration configuration, INewsRepo repository, IMapper mapper)
        {
            _configuration = configuration;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select Id, Title, DateOfPublication, NewsContent from dbo.News";
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

        [Authorize(Policy = "RequireAdministratorRole")]
        [Authorize(Policy = "RequireEmployeeRole")]
        [HttpPost]
        public JsonResult Post(News news)
        {
            string query = @"
                insert into dbo.News values
                (N'" + news.Title + @"', N'" + news.DateOfPublication + @"',
                 N'" + news.NewsContent + @"')";

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
                return new JsonResult("Dodano aktualność pomyślnie");
            }
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [Authorize(Policy = "RequireEmployeeRole")]
        [HttpPut]
        public JsonResult Put(News news)
        {
            string query = @"
                update dbo.News set
                Title = N'" + news.Title + @"',
                DateOfPublication = '" + news.DateOfPublication + @"',
                NewsContent = '" + news.NewsContent + @"'
                where Id = " + news.Id + @"";

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
                return new JsonResult("Zaktualizowano aktualność pomyślnie");
            }
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [Authorize(Policy = "RequireEmployeeRole")]
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                delete from dbo.News
                where Id = " + id + @"";

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
                return new JsonResult("Usunięto aktualność pomyślnie");
            }
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [Authorize(Policy = "RequireEmployeeRole")]
        // search news by its ID
        //GET api/news
        [HttpGet("{id}", Name = "GetNewsById")]
        public ActionResult<NewsReadDto> GetNewsById(int id)
        {
            var newsId = _repository.GetNewsById(id);
            if (newsId != null)
                return Ok(_mapper.Map<NewsReadDto>(newsId));
            return NotFound();
        }



        [Authorize(Policy = "RequireAdministratorRole")]
        [Authorize(Policy = "RequireEmployeeRole")]
        // PATCH
        //PATCH api/news
        [HttpPatch("{id}")]
        public ActionResult PartialNewsUpdate(int id, JsonPatchDocument<NewsAddDto> patchDoc)
        {
            var newsFromRepo = _repository.GetNewsById(id);
            if (newsFromRepo == null)
                return NotFound();

            var newsToPatch = _mapper.Map<NewsAddDto>(newsFromRepo);
            patchDoc.ApplyTo(newsToPatch, ModelState);

            _mapper.Map(newsToPatch, newsFromRepo);
            _repository.UpdateNews(newsFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }


    }
}

/*
 * // get all news
        //GET api/news
    //    [HttpGet]
    //    public ActionResult <IEnumerable<NewsReadDto>> GetAllNews()
    //    {
    //        var news = _repository.GetAllNews();
    //        return Ok(_mapper.Map<IEnumerable<NewsReadDto>>(news));
    //    }
// add new news
        //POST api/news
        [HttpPost]
        public ActionResult<NewsAddDto> AddNews(NewsAddDto newsAddDto)
        {
            var newsModel = _mapper.Map<News>(newsAddDto);
            _repository.AddNews(newsModel);
            _repository.SaveChanges();
            var newsReadDto = _mapper.Map<NewsReadDto>(newsModel);
            return CreatedAtRoute(nameof(GetNewsById), new { Id = newsReadDto.Id }, newsReadDto);
        }
// update news content by its id
        //PUT api/news
        [HttpPut("{id}")]
        public ActionResult UpdateNews(int id, NewsAddDto newsAddDto)
        {
            var newsFromRepo = _repository.GetNewsById(id);
            if (newsFromRepo == null)
                return NotFound();
            _mapper.Map(newsAddDto, newsFromRepo);
            _repository.UpdateNews(newsFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
//DELETE api/news
        [HttpDelete("{id}")]
        public ActionResult DeleteNews(int id)
        {
            var newsFromRepo = _repository.GetNewsById(id);
            if (newsFromRepo == null)
                return NotFound();
            _repository.DeleteNews(newsFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
 *
 *
 */