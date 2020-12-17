using JamCentral.Models;
using System.Collections.Generic;

namespace JamCentral.Repositories
{
    public interface IFollowingsRepository
    {
        ICollection<ApplicationUser> GetFollowersByArtist(string artistId);
    }
}