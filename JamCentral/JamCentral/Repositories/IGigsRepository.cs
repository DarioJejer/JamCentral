﻿using JamCentral.Models;
using System.Collections.Generic;

namespace JamCentral.Repositories
{
    public interface IGigsRepository
    {
        Gig GetGig(int gigId);
        IEnumerable<Gig> GetGigsOfArtist(string userId);
        IEnumerable<Gig> GetGigsUserIsAttending(string userId);
        Gig GetGigWithAttendanceAndFolllowers(int gigId);
        IEnumerable<Gig> GetAllUpcomingGigs();
        IEnumerable<Gig> GetGigsOfSearch(string search);
        void Add(Gig gig);
    }
}