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

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("Create", viewModel);
            }

            var Gig = new Gig
            {
                Location = viewModel.Location,
                Date = viewModel.GetDateTime(),
                GenreId = viewModel.GenreId,
                ArtistId = User.Identity.GetUserId()
            };

            _context.Gigs.Add(Gig);

            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}