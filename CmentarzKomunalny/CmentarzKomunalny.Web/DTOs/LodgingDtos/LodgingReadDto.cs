using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmentarzKomunalny.Web.DTOs.LodgingDtos
{
    public class LodgingReadDto
    {
        public int IdLodge { get; set; }
        public bool isReserved { get; set; }
        public int PeopleLimit { get; set; }
    }
}
