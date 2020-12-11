using System.ComponentModel.DataAnnotations;

namespace CmentarzKomunalny.Web.Models.Cmentarz
{
    public class Graveyard
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
