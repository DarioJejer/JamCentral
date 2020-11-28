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
    }
}
