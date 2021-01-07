using JamCentral.Models;
using JamCentral.Repositories;

namespace JamCentral.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IGigsRepository Gigs { get; private set; }
        public IUserRepository Users { get; private set; }
        public IFollowingsRepository Followings { get; private set; }
        public IGenresRepository Genres { get; private set; }
        public IAttendencesRepository Attendences { get; private set; }




        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigsRepository(_context);
            Users = new UserRepository(_context);
            Followings = new FollowingsRepository(_context);
            Genres = new GenresRepository(_context);
            Attendences = new AttendencesRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}