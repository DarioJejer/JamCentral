using JamCentral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JamCentral.Repositories
{
    public class GenresRepository : IGenresRepository
    {
        private ApplicationDbContext _context;

        public GenresRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }
    }
}