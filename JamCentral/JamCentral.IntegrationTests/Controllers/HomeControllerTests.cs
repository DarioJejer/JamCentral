using AutoMapper;
using FluentAssertions;
using JamCentral.App_Start;
using JamCentral.Controllers;
using JamCentral.IntegrationTests.Extensions;
using JamCentral.Models;
using JamCentral.Persistence;
using JamCentral.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JamCentral.IntegrationTests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        private ApplicationDbContext _context;
        private HomeController _controller;
        private ApplicationUser _user;
        private Genre _genre;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            var unitOfWork = new UnitOfWork(_context);
            _controller = new HomeController(unitOfWork);
            _user = _context.Users.First();
            _controller.MockCurrentUser(_user.Id, _user.UserName);
            _genre = _context.Genres.First();
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());
        }

        [TearDown]
        public void TearDown()
        {
            Mapper.Reset();
            _context.Dispose();
        }

        [Test, Isolated]
        public void Index_CalledWithLogInUser_ReturnGigsAndAttendances()
        {
            var gig1 = new Gig(_user.Id, "-", DateTime.Now.AddDays(1), _genre.Id);
            var gig2 = new Gig(_user.Id, "-", DateTime.Now.AddDays(1), _genre.Id);
            var gigs = new List<Gig>();
            gigs.Add(gig1);
            gigs.Add(gig2);
            _context.Gigs.AddRange(gigs);
            _context.SaveChanges();

            _context.Entry(gig1).Reload();
            var attendance = new Attendence()
            {
                AttendeeId = _user.Id,
                GigId = gig1.Id
            };
            _context.Attendences.Add(attendance);
            _context.SaveChanges();

            var result = _controller.Index();

            (result.ViewData.Model as GigsViewModel).Attendences.Should().HaveCount(1);
            (result.ViewData.Model as GigsViewModel).upcomingGigs.Should().HaveCount(2);
        }

        [Test, Isolated]
        public void Search_CalledCorrectly_ReturnSearchResults()
        {
            var gig = new Gig(_user.Id, "Test Concert", DateTime.Now.AddDays(1), _genre.Id);
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            _context.Entry(gig).Reload();
            var attendance = new Attendence()
            {
                AttendeeId = _user.Id,
                GigId = gig.Id
            };
            _context.Attendences.Add(attendance);
            _context.SaveChanges();

            var result = _controller.Search("Concert");

            (result.ViewData.Model as GigsViewModel).upcomingGigs.Should().HaveCount(1);
            (result.ViewData.Model as GigsViewModel).Attendences.Should().HaveCount(1);
        }
    }
}
