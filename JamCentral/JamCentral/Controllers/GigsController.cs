using AutoMapper;
using JamCentral.Dtos;
using JamCentral.Models;
using JamCentral.Repositories;
using JamCentral.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace JamCentral.Controllers
{
    public class GigsController : Controller
    {
        private ApplicationDbContext _context;
        private GigsRepository _gigsRepository;
        private UserRepository _userRepository;
        private FollowingsRepository _followingsRepository;

        public GigsController()
        {
            _context = new ApplicationDbContext();
            _gigsRepository = new GigsRepository(_context);
            _userRepository = new UserRepository(_context);
            _followingsRepository = new FollowingsRepository(_context);
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

            var artistId = User.Identity.GetUserId();

            var gig = new Gig(artistId, viewModel.Location, viewModel.GetDateTime(), viewModel.GenreId);

            _context.Gigs.Add(gig);

            var followers = _followingsRepository.GetFollowersByArtist(artistId);

            gig.NotifyGigCreation(followers);

            _context.SaveChanges();            

            return RedirectToAction("Mine", "Gigs");
        }
        [Authorize]
        public ActionResult MyCalendar()
        {
            var userId = User.Identity.GetUserId();

            var viewModel = new GigsViewModel
            {
                upcomingGigs = _gigsRepository.GetGigsUserIsAttending(userId),
                showActions = true,
                User = Mapper.Map<ApplicationUserDto>(_userRepository.GetUser(userId)),
                Title = "Gigs that you are attending",
                Header = "My calendar"
            };

            return View(viewModel);
        }           

        [Authorize]
        public ActionResult Edit(int gigId)
        {
            var gig = _gigsRepository.GetGig(gigId);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();
            
            var viewModel = new GigFormViewModel
            {
                Id = gig.Id,
                Heading = "Edit a Gig",
                Location = gig.Location,
                Date = gig.Date.ToString("d MMM yyyy"),
                Time = gig.Date.ToString("HH:mm"),
                GenreId = gig.GenreId,
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

            var gig = _gigsRepository.GetGigWithAttendanceAndFolllowers(viewModel.Id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            gig.Modify(viewModel.GetDateTime(), viewModel.Location, viewModel.GenreId);            

            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();

            var gigs = _gigsRepository.GetGigsOfUser(userId);

            return View(gigs);
        }       
    }
}