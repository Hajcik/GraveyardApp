using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmentarzKomunalny.Web.DTOs.DeadPersonDtos
{
    public class DeadPersonAddDto
    {
        [ForeignKey("IdLodge")]
        [Required]
        public int LodgingId { get; set; }

        [Required]
        [MaxLength(75)]
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        [Required]
        public string DateOfDeath { get; set; }
        
    }
}
