using JamCentral.Dtos;
using JamCentral.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace JamCentral.Controllers.API
{
    [Authorize]
    public class AttendencesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendencesController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult Attend(AttendenceDto dto)
        {
            var userId = User.Identity.GetUserId();
            var recordExistInDb = _context.Attendences.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId);

            if (recordExistInDb)
                return BadRequest("The atendence already exist");

            var attendence = new Attendence
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            _context.Attendences.Add(attendence);
            _context.SaveChanges();

            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public IHttpActionResult Unbook(AttendenceDto dto)
        {
            var userId = User.Identity.GetUserId();
            var recordInDb = _context.Attendences.SingleOrDefault(a => a.AttendeeId == userId && a.GigId == dto.GigId);

            if (recordInDb == null)
                return BadRequest("The atendence doesn't exist");

            _context.Attendences.Remove(recordInDb);

            _context.SaveChanges();

            return Ok();
        }
    }
}
