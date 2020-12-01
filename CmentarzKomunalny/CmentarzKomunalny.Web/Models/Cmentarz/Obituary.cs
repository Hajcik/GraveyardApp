using System;
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
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        [Range(typeof(DateTime), "01/01/1900", "01/01/2100", ErrorMessage = "Date is out of range")]
        public DateTime DateOfDeath_Obituary { get; set; }
        
    }
}
