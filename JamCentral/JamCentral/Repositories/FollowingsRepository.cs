using JamCentral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JamCentral.Repositories
{
    public class FollowingsRepository : IFollowingsRepository
    {
        private ApplicationDbContext _context;

        public FollowingsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<ApplicationUser> GetFollowersByArtist(string artistId)
        {
            return _context.Followings
                .Where(f => f.ArtistId == artistId)
                .Select(f => f.User)
                .ToList();
        }
    }
}