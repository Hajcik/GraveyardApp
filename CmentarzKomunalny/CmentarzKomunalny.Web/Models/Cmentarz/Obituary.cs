using System.ComponentModel.DataAnnotations;

namespace CmentarzKomunalny.Web.Models.Cmentarz
{
    // en - nekrolog
    public class Obituary
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(70)]
        public string Name { get; set; }

        [Required]
        public string ObituaryContent { get; set; }

        [Required]
        public string DateOfDeath_Obituary { get; set; }
        
    }
}
