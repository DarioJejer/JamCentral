using FluentAssertions;
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
    public class GigsControllerTests
    {
        private ApplicationDbContext _context;
        private GigsController _controller;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new GigsController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test, Isolated]
        public void Mine_CalledCorrectly_ReturnGigs()
        {
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);
            var genre = _context.Genres.Single(g => g.Id == 1);
            var gig = new Gig(user.Id, "-", DateTime.Now.AddDays(1), genre.Id);
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            var result = _controller.Mine();

            (result.ViewData.Model as IEnumerable<Gig>).Should().HaveCount(1);
        }

        [Test, Isolated]
        public void Update_CalledCorrectly_ModifyThegigOnDb()
        {
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);
            var genre = _context.Genres.Single(g => g.Id == 1);
            var gig = new Gig(user.Id, "-", DateTime.Now.AddDays(1), genre.Id);
            _context.Gigs.Add(gig);
            _context.SaveChanges();
            var viewModel = new GigFormViewModel
            {
                Id = gig.Id,
                Location = "new",
                Date = DateTime.Today.AddDays(2).ToString("d MMM yyyy"),
                Time = "20:00",
                GenreId = 2
            };

            var result = _controller.Update(viewModel);
            
            _context.Entry(gig).Reload();
            gig.Date.Should().Be(DateTime.Today.AddDays(2).AddHours(20));
            gig.Location.Should().Be("new");
            gig.GenreId.Should().Be(2);

        }
    }
}
