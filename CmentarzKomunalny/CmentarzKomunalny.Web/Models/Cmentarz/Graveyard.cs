using System;
using System.ComponentModel.DataAnnotations;

namespace CmentarzKomunalny.Web.Models.Cmentarz
{
    public class Graveyard
    {
        public int LimitLodgings { get; set; }
        public int LimitColumbariums { get; set; }
        // check if they needs "static" later on
  /*      [Required]
        public int AmountLodgings
        { 
            get => limitLodgings;
            set => limitLodgings = value >= 1 && value <= 200
                ? value
                : throw new ArgumentOutOfRangeException("More or less lodgings than expected");
        }
        // you can count up the amount by adding up all the IDs of lodgings on graveyard

        [Required]
        public int AmountColumbariums
        {
            get => limitColumbariums;
            set => limitColumbariums = value >= 0 && value <= 5
                ? value
                : throw new ArgumentOutOfRangeException("More or less columbariums than expected");
        }
    */
        // you can count up the amount by adding up all the IDs of columbariums on graveyard
    }
    // check if we need [Required] status later on

}   
