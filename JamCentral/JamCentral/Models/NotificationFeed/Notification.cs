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
        public string GigPreviousLocation { get; set; }
        public DateTime? GigPreviousDateTime { get; set; }
        [Required]
        public NotificationType Type { get; private set; }

        protected Notification()
        {}

        public Notification(Gig gig, NotificationType type)
        {
            if (gig == null)
                throw new ArgumentNullException("Gig");
            if (type == 0)
                throw new ArgumentNullException("Type");

            Gig = gig;
            Type = type;
            NotificationDateTime = DateTime.Now;
        }
    }


}