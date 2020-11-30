using JamCentral.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JamCentral.ViewModels
{
    public class GigFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        [Display(Name ="Genre")]    
        public byte GenreId { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public string Action 
        {
            get {
                return (Id == 0) ? "Create" : "Update";   
            }
        }

        public string Heading { get; set; }

        public DateTime GetDateTime ()
        { 
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }
    }
}