using JamCentral.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace JamCentral.Repositories
{
    public class GigsRepository : IGigsRepository
    {
        private IApplicationDbContext _context;

        public GigsRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Gig> GetGigsOfArtist(string userId)
        {
            return _context.Gigs
                .Where(g => g.ArtistId == userId && g.Date > DateTime.Now)
                .Include(g => g.Genre)
                .OrderBy(g => g.Date)
                .ToList();
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

        public Gig GetGig(int gigId)
        {
            return _context.Gigs.SingleOrDefault(g => g.Id == gigId);
        }

        public Gig GetGigWithAttendanceAndFolllowers(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Artist.Followers.Select(f => f.User))
                .Include(g => g.Attendences.Select(a => a.Attendee))
                .SingleOrDefault(g => g.Id == gigId);
        }

        public IEnumerable<Gig> GetAllUpcomingGigs()
        {
            return _context.Gigs
                .Include(m => m.Artist)
                .Include(m => m.Genre)
                .Where(g => g.Date > DateTime.Now && !g.IsCanceled)
                .OrderBy(g => g.Date)
                .ToList();
        }

        public IEnumerable<Gig> GetGigsOfSearch(string search)
        {
            return _context.Gigs
                .Include(m => m.Artist)
                .Include(m => m.Genre)
                .Where(g =>
                g.Date > DateTime.Now &&
                !g.IsCanceled && (
                g.Artist.Name.Contains(search) ||
                g.Genre.Name.Contains(search) ||
                g.Location.Contains(search)
                ))
                .ToList();
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }
    }
}