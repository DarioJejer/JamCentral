using JamCentral.Models;
using JamCentral.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JamCentral.Persistence
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public GigsRepository Gigs {get; private set;}
        public UserRepository Users { get; private set; }
        public FollowingsRepository Followings { get; private set; }
        public GenresRepository Genres { get; private set; }




        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigsRepository(_context);
            Users = new UserRepository(_context);
            Followings = new FollowingsRepository(_context);
            Genres = new GenresRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}