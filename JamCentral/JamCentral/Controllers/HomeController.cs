using AutoMapper;
using JamCentral.Dtos;
using JamCentral.Models;
using JamCentral.Persistence;
using JamCentral.ViewModels;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace JamCentral.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork _unitOfWok;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWok = unitOfWork;
        }
        public ActionResult Index()
        {

            var gigs = _unitOfWok.Gigs.GetAllUpcomingGigs();

            var userId = User.Identity.GetUserId();

            var user = new ApplicationUser();

            if (userId != null)            
                user = _unitOfWok.Users.GetUser(userId);

            var attendences = _unitOfWok.Attendences.GetAttendacesByUser(userId);

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
            var gigs = _unitOfWok.Gigs.GetGigsOfSearch(search);

            var userId = User.Identity.GetUserId();

            var user = new ApplicationUser();

            var attendences = _unitOfWok.Attendences.GetAttendacesByUser(userId); ;

            if (userId != null)
                user = _unitOfWok.Users.GetUser(userId);

            var viewModel = new GigsViewModel
            {
                upcomingGigs = gigs,
                showActions = User.Identity.IsAuthenticated,
                User = Mapper.Map<ApplicationUserDto>(user),
                Title = "Upcoming Gigs for this season",
                Header = "Home Page",
                Search = search,
                Attendences = attendences
            };

            return View("../Gigs/GigsList", viewModel);
        }
    }
}