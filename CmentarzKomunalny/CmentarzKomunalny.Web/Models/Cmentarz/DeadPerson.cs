using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CmentarzKomunalny.Web.Models.Cmentarz
{
    public class DeadPerson
    {
        // persons Id
        [Key]
        public int Id { get; set; }

        [ForeignKey("Lodging")]
        [Required]
        public int? LodgingId { get; set; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(45)]
        public string LastName { get; set; }

        [DataType(DataType.DateTime)]
        [Range(typeof(DateTime), "01/01/1900", "01/01/2100", ErrorMessage = "Date is out of range")]
        public DateTime DateOfBirth { get; set; }
       
        [Required]
        [DataType(DataType.DateTime)]
        [Range(typeof(DateTime), "01/01/1900", "01/01/2100", ErrorMessage = "Date is out of range")]
        public DateTime? DateOfDeath { get; set; }
    }
}

// ? means it's not nullable