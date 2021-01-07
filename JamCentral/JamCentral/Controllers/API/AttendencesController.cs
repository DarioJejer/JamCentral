using JamCentral.Dtos;
using JamCentral.Models;
using JamCentral.Persistence;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace JamCentral.Controllers.API
{
    [Authorize]
    public class AttendencesController : ApiController
    {
        private ApplicationDbContext _context;
        private IUnitOfWork _unitOfWork;

        public AttendencesController(IUnitOfWork unitOfWork)
        {
            _context = new ApplicationDbContext();
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult Attend(AttendenceDto dto)
        {
            var userId = User.Identity.GetUserId();
            var recordExistInDb = _unitOfWork.Attendences.GetAttendenceExistInDb(userId, dto.GigId);

            if (recordExistInDb)
                return BadRequest("The atendence already exist");

            var attendence = new Attendence
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            _unitOfWork.Attendences.Add(attendence);
            _unitOfWork.Complete();

            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public IHttpActionResult Unbook(AttendenceDto dto)
        {
            var userId = User.Identity.GetUserId();
            var recordInDb = _unitOfWork.Attendences.GetAttendenceByUserAndGig(userId, dto.GigId);

            if (recordInDb == null)
                return BadRequest("The atendence doesn't exist");

            _unitOfWork.Attendences.Remove(recordInDb);

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
