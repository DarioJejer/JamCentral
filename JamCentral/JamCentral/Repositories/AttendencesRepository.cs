using JamCentral.Models;
using System.Linq;

namespace JamCentral.Repositories
{
    public class AttendencesRepository
    {
        private ApplicationDbContext _context;

        public AttendencesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool GetAttendenceExistInDb(string userId, int GigId)
        {
            return _context.Attendences.Any(a => a.AttendeeId == userId && a.GigId == GigId);
        }

        public void Add(Attendence attendence)
        {
            _context.Attendences.Add(attendence);
        }

    }
}