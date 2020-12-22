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
        private IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpPut]
        public IHttpActionResult Uncancel(int id)
        {
            var gig = _unitOfWork.Gigs.GetGigWithAttendanceAndFolllowers(id);

            if (gig == null)
                return NotFound();

            if (!gig.IsCanceled)
                return BadRequest();

            if (gig.ArtistId != User.Identity.GetUserId())
                return Unauthorized();

            gig.Uncancel();

            _unitOfWork.Complete();

            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var gig = _unitOfWork.Gigs.GetGigWithAttendanceAndFolllowers(id);

            if (gig == null)
                return NotFound();

            if (gig.IsCanceled)
                return BadRequest();

            if (gig.ArtistId != User.Identity.GetUserId())
                return Unauthorized();

            gig.Cancel();

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
