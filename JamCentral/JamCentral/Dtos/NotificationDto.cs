using JamCentral.Models.NotificationFeed;
using System;

namespace JamCentral.Dtos
{
    public class NotificationDto
    {
        public GigDto Gig { get; set; }
        public DateTime NotificationDateTime { get; set; }
        public string GigPreviousLocation { get; set; }
        public DateTime? GigPreviousDateTime { get; set; }
        public NotificationType Type { get; set; }
    }
}