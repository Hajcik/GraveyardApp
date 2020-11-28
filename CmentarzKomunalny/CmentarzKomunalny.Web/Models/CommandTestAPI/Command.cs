using System.ComponentModel.DataAnnotations;

namespace CmentarzKomunalny.Web.Models.CommandTestAPI
{
    public class Command
    {
        [Key] // not necessary
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string HowTo { get; set; }

        [Required]
        public string Line { get; set; }

        [Required]
        public string Platform { get; set; }
    }
}
