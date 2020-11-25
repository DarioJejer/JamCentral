using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JamCentral.Models;
using JamCentral.ViewModels;

namespace JamCentral.Controllers
{
    public class GigsController : Controller
    {
        // GET: Gigs
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = new List<Genre>
                {
                    new Genre { Id = 1, Name = "Jazz"},
                    new Genre { Id = 2, Name = "Rock"},
                    new Genre { Id = 3, Name = "Blues"}
                }
            };

            return View(viewModel);
        }
    }
}