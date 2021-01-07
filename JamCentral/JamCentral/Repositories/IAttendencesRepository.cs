using JamCentral.Models;

namespace JamCentral.Repositories
{
    public interface IAttendencesRepository
    {
        void Add(Attendence attendence);
        Attendence GetAttendenceByUserAndGig(string userId, int GigId);
        bool GetAttendenceExistInDb(string userId, int GigId);
        void Remove(Attendence attendence);
    }
}