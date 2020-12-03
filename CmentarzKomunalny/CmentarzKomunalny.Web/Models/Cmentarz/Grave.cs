using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CmentarzKomunalny.Web.Models.Cmentarz
{
    public enum GraveType
    {
        Empty = 0,
        Coffin = 1,
        Urn = 2
    }
    public class Grave
    { 
        [Key]
        public int IdGrave { get; set; }

        [Required]
        public bool isReserved { get; set; }

//        [Required]
//        public bool isBricked { get; set; }

        [Required]
        public GraveType graveType { get; set; }

    }
}
