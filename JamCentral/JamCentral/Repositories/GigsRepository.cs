using JamCentral.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JamCentral.Repositories
{
    public class GigsRepository
    {
        private ApplicationDbContext _context;

        public GigsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Gig> GetGigsUserIsAttending(string userId)
        {
            return _context.Attendences
                .Where(a => a.AttendeeId == userId && a.Gig.Date > DateTime.Now)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .OrderBy(g => g.Date)
                .ToList();
        }

        public Gig GetGigWithAttendanceAndFolllowers(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Artist.Followers.Select(f => f.User))
                .Include(g => g.Attendences.Select(a => a.Attendee))
                .SingleOrDefault(g => g.Id == gigId);
        }
    }
}