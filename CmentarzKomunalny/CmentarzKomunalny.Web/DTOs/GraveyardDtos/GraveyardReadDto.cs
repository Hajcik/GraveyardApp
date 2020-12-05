using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmentarzKomunalny.Web.DTOs.GraveyardDtos
{
    public class GraveyardReadDto
    {
        public int LimitLodgings { get; set; }
        public int LimitColumbariums { get; set; }
        public int LimitSectors { get; set; }
    }
}
