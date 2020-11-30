using System;
using System.ComponentModel.DataAnnotations;

namespace JamCentral.Models
{
    public class Gig
    {
        public int Id { get; set; }
        public ApplicationUser Artist { get; set; }

        [Required]
        public string ArtistId { get; set; }
        public DateTime Date { get; set; }

        public Genre Genre { get; set; }

        [Required]
        public byte GenreId { get; set; }

        [Required]
        [StringLength(255)]
        public string Location { get; set; }

        public bool IsCanceled { get; set; }
    }
}