using JamCentral.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JamCentral.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public ApplicationUser GetUser(string userId)
        {
            return _context.Users
                .Include(u => u.Followees)
                .Include(u => u.Attendences)
                .Single(u => u.Id == userId);
        }
    }
}