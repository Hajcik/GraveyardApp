using System;
using System.ComponentModel.DataAnnotations;

namespace CmentarzKomunalny.Web.Models.Cmentarz
{
    public class Columbarium
    {

        [Key]
        public int Id { get; set; }
        
        [Required]
        public int LimitUrns { get; set; }
      
    }
}
