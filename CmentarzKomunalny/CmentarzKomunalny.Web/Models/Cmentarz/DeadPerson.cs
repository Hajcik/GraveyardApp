using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CmentarzKomunalny.Web.Models.Cmentarz
{
    public class DeadPerson
    {
        // persons Id
        [Key]
        public int IdDeadPerson { get; set; }

        [ForeignKey("IdLodge")]
        [Required]
        public int LodgingId { get; set; }

        [Required]
        [MaxLength(75)]
        public string Name { get; set; }

        public string DateOfBirth { get; set; }
       
        [Required]
        public string DateOfDeath { get; set; }

    }
}

// ? means it's not nullable