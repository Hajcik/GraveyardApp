using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CmentarzKomunalny.Web.DTOs.NewsDtos
{
    public class NewsAddDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string NewsContent { get; set; }
        [Required]
        public string DateOfPublication { get; set; }
    }
}
