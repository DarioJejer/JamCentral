using JamCentral.Models;
using JamCentral.Models.NotificationFeed;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;

namespace JamCentral.Controllers.API
{
    [Authorize]
    public class GigsController : ApiController
    {
        private ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

            if (gig.IsCanceled)          
                return NotFound();            

            gig.IsCanceled = true;

            var notification = new Notification
            {
                NotificationDateTime = DateTime.Now,
                Gig = gig,
                Type = NotificationType.Canceled
            };

            var attendees = _context.Attendences
                .Where(a => a.GigId == gig.Id)
                .Select(a => a.Attendee)
                .ToList();

            foreach (var atendee in attendees)
            {
                var userNotification = new UserNotification
                {
                    Notification = notification,
                    User = atendee
                };
                _context.UserNotifications.Add(userNotification);
            }

            //_context.Notifications.Add(notification);

            _context.SaveChanges();

            return Ok();
        }
    }
}
