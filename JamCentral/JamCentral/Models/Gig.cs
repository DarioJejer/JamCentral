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

        public bool IsCanceled { get; private set; }

        public ICollection<Attendence> Attendences { get; set; }

        public void Cancel()
        {
            IsCanceled = true;

            var notification = Notification.GigCanceled(this);

            foreach (var follower in Artist.Followers.Select(f => f.User))
            {
                follower.Notify(notification);
            }

            foreach (var atendee in Attendences.Select(a => a.Attendee))
            {
                if (!Artist.Followers.Select(f => f.UserId).Any(i => i == atendee.Id))
                    atendee.Notify(notification);
            }
        }
        public void Uncancel()
        {
            IsCanceled = false;

            var notification = Notification.GigUncanceled(this);

            foreach (var follower in Artist.Followers.Select(f => f.User))
            {
                follower.Notify(notification);
            }

            foreach (var atendee in Attendences.Select(a => a.Attendee))
            {
                if (!Artist.Followers.Select(f => f.UserId).Any(i => i == atendee.Id))
                    atendee.Notify(notification);
            }
        }

        public void Modify(DateTime dateTime, string location, byte genreId, List<ApplicationUser> followers)
        {
            var notification = Notification.GigModified(this);

            Location = location;
            Date = dateTime;
            GenreId = genreId;

            foreach (var follower in followers)
            {
                follower.Notify(notification);
            }

            foreach (var atendee in Attendences.Select(a => a.Attendee))
            {
                if (!followers.Select(f => f.Id).Any(i => i == atendee.Id))
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