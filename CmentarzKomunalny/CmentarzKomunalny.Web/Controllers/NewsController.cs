using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CmentarzKomunalny.Web.DTOs.NewsDtos;
using CmentarzKomunalny.Web.Data.Interfaces;
using CmentarzKomunalny.Web.Models.Cmentarz;
using Microsoft.AspNetCore.JsonPatch;

namespace CmentarzKomunalny.Web.Controllers
{
    // api/news
    [Route("api/[controller]")] // polaczyc z 'aktualnosci'
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsRepo _repository;
        private readonly IMapper _mapper;
        
        public NewsController(INewsRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        // get all news
        //GET api/news
        [HttpGet]
        public ActionResult <IEnumerable<NewsReadDto>> GetAllNews()
        {
            var news = _repository.GetAllNews();
            return Ok(_mapper.Map<IEnumerable<NewsReadDto>>(news));
        }

        // search news by its ID
        //GET api/news
        [HttpGet("{id}", Name = "GetNewsById")]
        public ActionResult <NewsReadDto> GetNewsById(int id)
        {
            var newsId = _repository.GetNewsById(id);
            if (newsId != null)
                return Ok(_mapper.Map<NewsReadDto>(newsId));
            return NotFound();
        }
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
    }
}
