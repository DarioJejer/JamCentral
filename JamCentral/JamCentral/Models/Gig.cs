using System;
using System.ComponentModel.DataAnnotations;

namespace JamCentral.Models
{
    public class Gig
    {
        public int Id { get; set; }
        [Required]
        public ApplicationUser Artist { get; set; }
        public DateTime Date { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [Required]
        [StringLength(255)]
        public string Location { get; set; }
    }
}