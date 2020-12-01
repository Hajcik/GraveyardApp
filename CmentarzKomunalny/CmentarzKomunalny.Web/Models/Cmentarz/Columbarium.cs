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
        public int LimitUrns
        {
            get => limitUrns;
            set => limitUrns = value >= 0 && value <= 50
            ? value
            : throw new ArgumentOutOfRangeException("Too many or less urns than expected");
        }

    }
}
