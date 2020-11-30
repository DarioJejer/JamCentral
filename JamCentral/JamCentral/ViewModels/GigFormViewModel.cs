using JamCentral.Controllers;
using JamCentral.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

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
                Expression<Func<GigsController, ActionResult>> update = (c => c.Update(this));
                Expression<Func<GigsController, ActionResult>> create = (c => c.Create(this));
                var action = (Id == 0) ? create : update;

                return (action.Body as MethodCallExpression).Method.Name;   
            }
        }

        public string Heading { get; set; }

        public DateTime GetDateTime ()
        { 
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }
    }
}