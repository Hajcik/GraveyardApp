using System.ComponentModel.DataAnnotations;

namespace CmentarzKomunalny.Web.DTOs.GraveyardDtos
{
    public class GraveyardUpdateDto
    {
        [Key]
        public int IdGraveyard { get; set; }
        [Required]
        public int LimitLodgings { get; set; }
        [Required]
        public int LimitColumbariums { get; set; }
        [Required]
        public int LimitSectors { get; set; }
    }
}
