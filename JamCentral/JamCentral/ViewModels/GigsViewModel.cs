﻿using JamCentral.Dtos;
using JamCentral.Models;
using System.Collections.Generic;
using System.Linq;

namespace JamCentral.ViewModels
{
    public class GigsViewModel
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public ApplicationUserDto User { get; set; }
        public IEnumerable<Gig> upcomingGigs { get; set; }
        public bool showActions { get; set; }
        public string Search { get; set; }
        public ILookup<int, Attendence> Attendences { get; internal set; }
    }
}