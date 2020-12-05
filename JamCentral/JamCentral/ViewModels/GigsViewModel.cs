using JamCentral.Dtos;
using JamCentral.Models;
using System.Collections.Generic;

namespace JamCentral.ViewModels
{
    public class GigsViewModel
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public ApplicationUserDto User { get; set; }
        public IEnumerable<Gig> upcomingGigs { get; set; }
        public bool showActions { get; set; }
    }
}