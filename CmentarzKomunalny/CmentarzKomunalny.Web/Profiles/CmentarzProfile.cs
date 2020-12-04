using AutoMapper;
using CmentarzKomunalny.Web.DTOs.DeadPersonDtos;
using CmentarzKomunalny.Web.DTOs.LodgingDtos;
using CmentarzKomunalny.Web.DTOs.NewsDtos;
using CmentarzKomunalny.Web.DTOs.ObituaryDtos;
using CmentarzKomunalny.Web.Models.Cmentarz;

namespace CmentarzKomunalny.Web.Profiles
{
    public class CmentarzProfile : Profile
    {
        
        public CmentarzProfile()
        {
            // Dead People mapping
            // source -> target (deadperson -> read dto)
            CreateMap<DeadPerson, DeadPersonReadDto>();     // get
            CreateMap<DeadPersonAddDto, DeadPerson>();      // put / update      
            CreateMap<DeadPerson, DeadPersonAddDto>();      // patch

            // Lodgings mapping
            CreateMap<Lodging, LodgingReadDto>();           // get
            CreateMap<LodgingAddDto, Lodging>();            // put / update
            CreateMap<Lodging, LodgingAddDto>();            // patch

            // News mapping
            CreateMap<News, NewsReadDto>();                 // get
            CreateMap<NewsAddDto, News>();                  // put / update
            CreateMap<News, NewsAddDto>();                  // patch

            // Obituary mapping
            CreateMap<Obituary, ObituaryReadDto>();         // get
            CreateMap<ObituaryAddDto, Obituary>();          // put / update
            CreateMap<Obituary, ObituaryAddDto>();          // patch
        }
    }
}
