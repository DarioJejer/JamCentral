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

        public void Cancel()
        {
            IsCanceled = true;

            var notification = Notification.GigCanceled(this);

            foreach (var atendee in Attendences.Select(a => a.Attendee))
            {
                atendee.Notify(notification);
            }
        }

        public void Modify(DateTime dateTime, string location, byte genreId)
        {
            var notification = Notification.GigModified(this);

            Location = location;
            Date = dateTime;
            GenreId = genreId;


            foreach (var atendee in Attendences.Select(a => a.Attendee))
            {
                atendee.Notify(notification);
            }
        }

        public void NotifyGigCreation(ICollection<ApplicationUser> followers)
        {
            var notification = Notification.GigCreated(this);

            foreach (var follower in followers)
            {
                follower.Notify(notification);
            }
        }
    }
}