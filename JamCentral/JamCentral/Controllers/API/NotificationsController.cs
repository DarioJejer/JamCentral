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
    public class NotificationsController : ApiController
    {
        ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.BeenRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();


            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }
    }
}
