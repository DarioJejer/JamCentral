﻿using JamCentral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JamCentral.ViewModels
{
    public class HomeViewModel
    {        
        public IEnumerable<Gig> upcomingGigs { get; set; }
        public bool isAuthenticated { get; set; }
    }
}