using JamCentral.Repositories;

namespace JamCentral.Persistence
{
    public interface IUnitOfWork
    {
        IFollowingsRepository Followings { get; }
        IGenresRepository Genres { get; }
        IGigsRepository Gigs { get; }
        IUserRepository Users { get; }

        void Complete();
    }
}