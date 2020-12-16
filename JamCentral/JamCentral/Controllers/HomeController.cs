using AutoMapper;
using JamCentral.Dtos;
using JamCentral.Models;
using JamCentral.Repositories;
using JamCentral.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace JamCentral.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        private UserRepository _userRepository;

        public HomeController()
        {
            _context = new ApplicationDbContext();
            _userRepository = new UserRepository(_context);
        }
        public ActionResult Index()
        {

            var gigs = _context.Gigs
                .Include(m => m.Artist)
                .Include(m => m.Genre)
                .Where(g => g.Date > DateTime.Now && !g.IsCanceled)
                .OrderBy(g => g.Date)
                .ToList();

            var userId = User.Identity.GetUserId();

            var user = new ApplicationUser();

            if (userId != null)            
                user = _userRepository.GetUser(userId);
            
            var attendences = _context.Attendences
                .Where(a => a.AttendeeId == userId && a.Gig.Date > DateTime.Now)
                .ToList()
                .ToLookup(a => a.GigId);

            var viewModel = new GigsViewModel
            {
                upcomingGigs = gigs,
                showActions = User.Identity.IsAuthenticated,
                User = Mapper.Map<ApplicationUserDto>(user),
                Title = "Upcoming Gigs for this season",
                Header = "Home Page",
                Attendences = attendences
            };

            return View("../Gigs/GigsList", viewModel);
        }

        [HttpPost]
        public ActionResult Search(string search)
        {

            var gigs = _context.Gigs
                .Include(m => m.Artist)
                .Include(m => m.Genre)
                .Where(g =>
                g.Date > DateTime.Now &&
                !g.IsCanceled && (
                g.Artist.Name.Contains(search) ||
                g.Genre.Name.Contains(search) ||
                g.Location.Contains(search)
                ))
                .ToList();

            var userId = User.Identity.GetUserId();

            var user = new ApplicationUser();

            if (userId != null)
            {
                user = _context.Users
                    .Include(u => u.Followees)
                    .Include(u => u.Attendences)
                    .Single(u => u.Id == userId);
            }

            var viewModel = new GigsViewModel
            {
                upcomingGigs = gigs,
                showActions = User.Identity.IsAuthenticated,
                User = Mapper.Map<ApplicationUserDto>(user),
                Title = "Upcoming Gigs for this season",
                Header = "Home Page",
                Search = search

            };

            return View("../Gigs/GigsList", viewModel);
        }
    }
}