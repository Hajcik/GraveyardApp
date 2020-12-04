using System;
using System.ComponentModel.DataAnnotations;

namespace CmentarzKomunalny.Web.Models.Cmentarz
{
    public class Columbarium
    {
        private static int limitUrns;

        [Key]
        public int Id { get; set; }
        
        [Required]
        public int LimitUrns { get; set; }
        

    }
}
