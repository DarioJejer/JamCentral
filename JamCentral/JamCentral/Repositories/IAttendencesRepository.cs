using JamCentral.Models;
using System.Linq;

namespace JamCentral.Repositories
{
    public interface IAttendencesRepository
    {
        void Add(Attendence attendence);
        Attendence GetAttendenceByUserAndGig(string userId, int GigId);
        bool GetAttendenceExistInDb(string userId, int GigId);
        ILookup<int, Attendence> GetAttendacesByUser(string userId);
        void Remove(Attendence attendence);
    }
}