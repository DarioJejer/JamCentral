using JamCentral.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
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

        [Authorize]
        [HttpPut]
        public IHttpActionResult Uncancel(int id)
        {
            var userId = User.Identity.GetUserId();

            var gig = _context.Gigs
                .Include(g => g.Artist.Followers.Select(f => f.User))
                .Include(g => g.Attendences.Select(a => a.Attendee))
                .SingleOrDefault(g => g.Id == id && g.ArtistId == userId && g.IsCanceled);

            if (gig == null)
                return NotFound();

            gig.Uncancel();

            _context.SaveChanges();

            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs
                .Include(g => g.Attendences.Select(a => a.Attendee))
                .Include(g => g.Artist.Followers.Select(f => f.User))
                .SingleOrDefault(g => g.Id == id && g.ArtistId == userId && !g.IsCanceled);

            if (gig == null)          
                return NotFound();

            gig.Cancel();

            _context.SaveChanges();

            return Ok();
        }
    }
}
