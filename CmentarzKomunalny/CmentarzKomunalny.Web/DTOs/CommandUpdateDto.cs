using System.ComponentModel.DataAnnotations;

// it's basically the same as ..CreateDto, we're kind of doing a bad thing but in forms of
// presentation we're doing it to understand how stuff works, they are

// we're having two different classes because we may want to make some changes
// so we have a clear situation and know what we can do

namespace CmentarzKomunalny.Web.DTOs
{
    public class CommandUpdateDto
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
