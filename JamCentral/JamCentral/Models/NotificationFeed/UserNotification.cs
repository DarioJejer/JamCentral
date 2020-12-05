using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JamCentral.Models.NotificationFeed
{
    public class UserNotification
    {
        public ApplicationUser User { get; private set; }
        [Key]
        [Column(Order = 1)]
        public string UserId { get; private set; }
        public Notification Notification { get; private set; }

        [Key]
        [Column(Order = 2)]
        public int NotificationId { get; private set; }
        public bool BeenRead { get; private set; }

        protected UserNotification()
        {

        }
        public UserNotification(Notification notification, ApplicationUser user)
        {
            if (notification == null)
            {
                throw new ArgumentNullException("Notification");
            }
            if (user == null)
            {
                throw new ArgumentNullException("User");
            }

            Notification = notification;
            User = user;
        }

        internal void Read()
        {
            BeenRead = true;
        }
    }
}