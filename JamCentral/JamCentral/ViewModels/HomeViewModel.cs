using JamCentral.Models;
using System.Collections.Generic;

namespace JamCentral.ViewModels
{
    public class GigsViewModel
    {        
        public IEnumerable<Gig> upcomingGigs { get; set; }
        public bool showActions { get; set; }
    }
}