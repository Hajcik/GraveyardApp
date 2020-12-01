using System;
using System.ComponentModel.DataAnnotations;


namespace CmentarzKomunalny.Web.Models.Cmentarz
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        [Range(typeof(DateTime), "01/01/1900", "01/01/2100", ErrorMessage = "Date is out of range")]
        public DateTime DateOfPublication { get; set; }
    }
}
