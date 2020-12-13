using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmentarzKomunalny.Web.DTOs.LodgingDtos
{
    public class LodgingAddDto
    {
        public enum Type
        {
            Single = 0, // maximum 3 people for 20 years /without monument 2
            Double = 1  // maximum 6 people for 20 years /without monument 4
        }

        [Required]
        public Type LodgingType { get; set; }

        [Required]
        public bool isReserved { get; set; }

        [Required]
        public bool isMonument { get; set; }

        [Required]
        public int PeopleLimit { get; set; }
    }
}
