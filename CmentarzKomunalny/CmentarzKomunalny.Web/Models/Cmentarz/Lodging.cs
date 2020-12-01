using System;
using System.ComponentModel.DataAnnotations;

namespace CmentarzKomunalny.Web.Models.Cmentarz
{   // Lodging - Kwatera
    public class Lodging
    {
        public enum Type
        {
            Single = 0,
            Double = 1
        }

        [Key]
        public int Id { get; set; }
        // searching for it will be based on Identifier of exact lodging

        [Required]
        public Type LodgingType { get; set; }

        [Required]
        public Grave grave { get; set; }

        private static int limitOsob;
       
        [Required]
        public bool isMonument { get; set; }

        // check later for changes if it doesnt work properly
        [Required]
        public static int LimitOsob
        {

            get => limitOsob;
            set => limitOsob = value >= 0 && value <= 6
                    ? value 
                    : throw new ArgumentOutOfRangeException("Za dużo osób w grobie");
        }
    }
}

// if something wrong  happens, delete constructors, i'm not sure if they are necessary for project to work
