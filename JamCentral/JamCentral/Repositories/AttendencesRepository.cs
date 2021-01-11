using JamCentral.Models;
using System;
using System.Linq;

namespace JamCentral.Repositories
{
    public class AttendencesRepository : IAttendencesRepository
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

        public Attendence GetAttendenceByUserAndGig(string userId, int GigId)
        {
            return _context.Attendences.SingleOrDefault(a => a.AttendeeId == userId && a.GigId == GigId);

        }

        public ILookup<int, Attendence> GetAttendacesByUser(string userId)
        {
            return _context.Attendences
                .Where(a => a.AttendeeId == userId && a.Gig.Date > DateTime.Now)
                .ToList()
                .ToLookup(a => a.GigId);
        }

        public void Add(Attendence attendence)
        {
            _context.Attendences.Add(attendence);
        }

        public void Remove(Attendence attendence)
        {
            _context.Attendences.Remove(attendence);
        }

    }
}