using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmentarzKomunalny.Web.Models.Cmentarz
{   // Lodging - Kwatera
    public class Lodging
    {
        public enum Type
        {
            Single = 0, // maximum 3 people for 20 years /without monument 2
            Double = 1  // maximum 6 people for 20 years /without monument 4
        }

        [Key]
        public int IdLodge { get; set; }
        // searching for it will be based on Identifier of exact lodging
        [Required]
        public Type LodgingType { get; set; }

        [Required]
        public bool isReserved { get; set; }
       
        [Required]
        public bool isMonument { get; set; }

        [Required]
        public int PeopleLimit { get; set; }
        
    }
}

// if something wrong  happens, delete constructors, i'm not sure if they are necessary for project to work
