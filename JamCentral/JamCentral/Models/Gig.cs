using JamCentral.Models.NotificationFeed;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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

        public ICollection<Attendence> Attendences { get; set; }

        internal void Cancel()
        {

            IsCanceled = true;

            var notification = new Notification(this, NotificationType.Canceled);

            foreach (var atendee in Attendences.Select(a => a.Attendee))
            {
                atendee.Notify(notification);
            }
        }
    }
}