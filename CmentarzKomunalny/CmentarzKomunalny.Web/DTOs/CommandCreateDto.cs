using System.ComponentModel.DataAnnotations;

namespace CmentarzKomunalny.Web.DTOs
{
    public class CommandCreateDto
    {
        // the ID doesn't exist in ..CreateDto, it's handled by database
        // so it will be made by database itself!
        [Required]
        [MaxLength(250)]
        public string HowTo { get; set; }
        [Required]
        public string Line { get; set; }
        [Required]
        public string Platform { get; set; }
    }
}
