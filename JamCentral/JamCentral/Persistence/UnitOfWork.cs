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

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigsRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}