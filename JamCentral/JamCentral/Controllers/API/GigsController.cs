using JamCentral.Models;
using JamCentral.Persistence;
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
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _context = new ApplicationDbContext();
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpPut]
        public IHttpActionResult Uncancel(int id)
        {
            //var userId = User.Identity.GetUserId();

            var gig = _unitOfWork.Gigs.GetGigWithAttendanceAndFolllowers(id);

            if (gig == null)
                return NotFound();

            if (!gig.IsCanceled)
                return BadRequest();

            if (gig.ArtistId != User.Identity.GetUserId())
                return Unauthorized();

            //var gig = _context.Gigs
            //    .Include(g => g.Artist.Followers.Select(f => f.User))
            //    .Include(g => g.Attendences.Select(a => a.Attendee))
            //    .SingleOrDefault(g => g.Id == id && g.ArtistId == userId && g.IsCanceled);

            gig.Uncancel();

            _unitOfWork.Complete();

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
