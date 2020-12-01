using System;
using System.ComponentModel.DataAnnotations;

namespace JamCentral.Models.NotificationFeed
{
    public class Notification
    {
        public int Id { get; set; }
        [Required]
        public Gig Gig { get; set; }
        public int GigId { get; set; }
        public DateTime NotificationDateTime { get; set; }
        public string GigPreviousLocation { get; set; }
        public DateTime? GigPreviousDateTime { get; set; }
        [Required]
        public NotificationType Type { get; set; }
    }
}