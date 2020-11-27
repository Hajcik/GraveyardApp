using System.ComponentModel.DataAnnotations;

namespace CmentarzKomunalny.Web.DTOs
{
    public class CommandReadDto
    {
   //     [Key]
        public int Id { get; set; }

   //     [Required]
   //     [MaxLength(250)]
        public string HowTo { get; set; }

   //     [Required]
        public string Line { get; set; }

        //       [Required]
        //       public string Platform { get; set; } 
        // we dont need to expose that to our client
    }
}