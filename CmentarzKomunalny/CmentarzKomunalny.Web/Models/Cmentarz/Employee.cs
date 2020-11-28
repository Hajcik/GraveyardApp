using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace CmentarzKomunalny.Web.Models.Cmentarz
{
    
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Wprowadź swoje imię")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string FirstName { get; set; }

        [Required]
   //   [StringLength(30, ErrorMessage = "Wprowadź swoje nazwisko")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
    //  [Display(Name ="Województwo")]
        [StringLength(30)]
        public string Voivodeship { get; set; }
        [Required]
        [MaxLength(6)]
        public string PostalCode { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
