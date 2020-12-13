using System;
using System.ComponentModel.DataAnnotations;

namespace JamCentral.Models.NotificationFeed
{
    public class Notification
    {
        public int Id { get; private set; }
        [Required]
        public Gig Gig { get; private set; }
        public int GigId { get; set; }
        public DateTime NotificationDateTime { get; private set; }
        public string GigNewLocation { get; private set; }
        public DateTime? GigNewDateTime { get; private set; }
        public string GigPreviousLocation { get; private set; }
        public DateTime? GigPreviousDateTime { get; private set; }
        [Required]
        public NotificationType Type { get; private set; }

        protected Notification()
        {}

        private Notification(Gig gig, NotificationType type)
        {
            if (gig == null)
                throw new ArgumentNullException("Gig");
            if (type == 0)
                throw new ArgumentNullException("Type");

            Gig = gig;
            Type = type;
            NotificationDateTime = DateTime.Now;
        }

        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(gig, NotificationType.Canceled);
        }
        public static Notification GigCreated(Gig gig)
        {
            return new Notification(gig, NotificationType.Created);
        }
        public static Notification GigModified(Gig oldGig, string location, DateTime dateTime)
        {
            var notification = new Notification(oldGig, NotificationType.Modified);
            notification.GigPreviousLocation = oldGig.Location;
            notification.GigPreviousDateTime = oldGig.Date;
            notification.GigNewLocation = location;
            notification.GigNewDateTime = dateTime;
            return notification;
        }
        public static Notification GigUncanceled(Gig gig)
        {
            return new Notification(gig, NotificationType.Uncanceled);
        }
    }


}