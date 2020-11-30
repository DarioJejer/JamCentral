﻿using JamCentral.Models;
using JamCentral.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

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
                Heading = "Add a Gig",
                Genres = _context.Genres.ToList()
            };

            return View("GigForm",viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Heading = "Add a Gig";
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var gig = new Gig
            {
                Location = viewModel.Location,
                Date = viewModel.GetDateTime(),
                GenreId = viewModel.GenreId,
                ArtistId = User.Identity.GetUserId()
            };

            _context.Gigs.Add(gig);

            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }
        [Authorize]
        public ActionResult MyCalendar()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Attendences
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();

            var viewModel = new GigsViewModel
            {
                upcomingGigs = gigs,
                showActions = false
            };

            return View(viewModel);
        }

        [Authorize]
        public ActionResult Edit(int gigId)
        {
            var userId = User.Identity.GetUserId();
            var gigInDb = _context.Gigs.SingleOrDefault(g => g.Id == gigId && g.ArtistId == userId);
            var viewModel = new GigFormViewModel
            {
                Id = gigInDb.Id,
                Heading = "Edit a Gig",
                Location = gigInDb.Location,
                Date = gigInDb.Date.ToString("d MMM yyyy"),
                Time = gigInDb.Date.ToString("HH:mm"),
                GenreId = gigInDb.GenreId,
                Genres = _context.Genres.ToList()
            };

            return View("GigForm", viewModel);
        }
        
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Heading = "Edit a Gig";
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == viewModel.Id && g.ArtistId == userId);

            gig.Location = viewModel.Location;
            gig.Date = viewModel.GetDateTime();
            gig.GenreId = viewModel.GenreId;

            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs.Where(g => g.ArtistId == userId).Include(g => g.Genre).ToList();

            return View(gigs);
        }        
    }
}