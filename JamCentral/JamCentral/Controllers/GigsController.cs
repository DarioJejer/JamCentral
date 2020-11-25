using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JamCentral.Models;
using JamCentral.ViewModels;
using Microsoft.AspNet.Identity;

namespace JamCentral.Controllers
{
    public class GigsController : Controller
    {
        private ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            var genreInDb = _context.Genres.Single(g => g.Id == viewModel.GenreId);

            var userId = User.Identity.GetUserId();
            var artist = _context.Users.Single(u => u.Id == userId);

            var Gig = new Gig
            {
                Location = viewModel.Location,
                Date = DateTime.Parse(string.Format("{0} {1}", viewModel.Date, viewModel.Time)),
                Genre = genreInDb,
                Artist = artist
            };

            _context.Gigs.Add(Gig);

            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}