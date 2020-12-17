using JamCentral.Models;
using System.Collections.Generic;

namespace JamCentral.Repositories
{
    public interface IGenresRepository
    {
        IEnumerable<Genre> GetGenres();
    }
}