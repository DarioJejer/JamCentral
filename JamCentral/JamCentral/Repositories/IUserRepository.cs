using JamCentral.Models;

namespace JamCentral.Repositories
{
    public interface IUserRepository
    {
        ApplicationUser GetUser(string userId);
    }
}