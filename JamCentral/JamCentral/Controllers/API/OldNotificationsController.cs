using AutoMapper;
using JamCentral.Dtos;
using JamCentral.Models;
using JamCentral.Models.NotificationFeed;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace JamCentral.Controllers.API
{
    public class OldNotificationsController : ApiController
    {
        private ApplicationDbContext _context;

        public OldNotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<NotificationDto> GetOldNotifications()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId)
                .Select(un => un.Notification)
                .OrderByDescending(n => n.NotificationDateTime)
                .Take(count: 3)
                .Include(n => n.Gig.Artist)
                .ToList();


            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }
    }
}
