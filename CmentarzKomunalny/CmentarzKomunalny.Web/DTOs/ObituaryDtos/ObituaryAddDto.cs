using System.ComponentModel.DataAnnotations;

namespace CmentarzKomunalny.Web.DTOs.ObituaryDtos
{
    public class ObituaryAddDto
    {
        [Required]
        [MaxLength(70)]
        public string Name { get; set; }

        [Required]
        public string ObituaryContent { get; set; }

        [Required]
        public string DateOfDeath_Obituary { get; set; }
    }
}
