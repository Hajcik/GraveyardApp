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
        public string NewsContent { get; set; }

        [Required]
        public string DateOfPublication { get; set; }
    }
}
