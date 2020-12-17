using JamCentral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JamCentral.Persistence
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}