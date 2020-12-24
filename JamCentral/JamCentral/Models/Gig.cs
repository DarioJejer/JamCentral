using JamCentral.Models.NotificationFeed;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace JamCentral.Models
{
    public class Gig
    {
        public int Id { get; private set; }
        public ApplicationUser Artist { get; set; }
        public string ArtistId { get; private set; }
        public DateTime Date { get; private set; }
        public Genre Genre { get; private set; }
        public byte GenreId { get; private set; }
        public string Location { get; private set; }
        public bool IsCanceled { get; private set; }
        public ICollection<Attendence> Attendences { get; private set; }

        private Gig()
        {
            Attendences = new Collection<Attendence>();
        }
        public Gig(string artistId, string location, DateTime date, byte genreId) : this()
        {            
            Date = date;
            ArtistId = artistId;
            GenreId = genreId;
            Location = location;
        }

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

        public void Modify(DateTime dateTime, string location, byte genreId)
        {
            var notification = Notification.GigModified(this, location, dateTime);

            Location = location;
            Date = dateTime;
            GenreId = genreId;

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