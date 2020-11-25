using JamCentral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JamCentral.ViewModels
{
    public class GigFormViewModel
    {
        public string Location { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public byte GenreId { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}