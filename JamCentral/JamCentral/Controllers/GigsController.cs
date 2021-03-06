﻿using AutoMapper;
using JamCentral.Dtos;
using JamCentral.Models;
using JamCentral.Persistence;
using JamCentral.ViewModels;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace JamCentral.Controllers
{
    public class GigsController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Heading = "Add a Gig",
                Genres = _unitOfWork.Genres.GetGenres()
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
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                return View("GigForm", viewModel);
            }

            var artistId = User.Identity.GetUserId();

            var gig = new Gig(artistId, viewModel.Location, viewModel.GetDateTime(), viewModel.GenreId);

            _unitOfWork.Gigs.Add(gig);

            var followers = _unitOfWork.Followings.GetFollowersByArtist(artistId);

            gig.NotifyGigCreation(followers);

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }
        [Authorize]
        public ActionResult MyCalendar()
        {
            var userId = User.Identity.GetUserId();

            var viewModel = new GigsViewModel
            {
                upcomingGigs = _unitOfWork.Gigs.GetGigsUserIsAttending(userId),
                showActions = true,
                User = Mapper.Map<ApplicationUserDto>(_unitOfWork.Users.GetUser(userId)),
                Title = "Gigs that you are attending",
                Header = "My calendar"
            };

            return View(viewModel);
        }           

        [Authorize]
        public ActionResult Edit(int gigId)
        {
            var gig = _unitOfWork.Gigs.GetGig(gigId);

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
                Genres = _unitOfWork.Genres.GetGenres()
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
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = _unitOfWork.Gigs.GetGigWithAttendanceAndFolllowers(viewModel.Id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            gig.Modify(viewModel.GetDateTime(), viewModel.Location, viewModel.GenreId);

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ViewResult Mine()
        {
            var userId = User.Identity.GetUserId();

            var gigs = _unitOfWork.Gigs.GetGigsOfArtist(userId);

            return View(gigs);
        }       
    }
}