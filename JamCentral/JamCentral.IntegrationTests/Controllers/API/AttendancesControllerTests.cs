using JamCentral.IntegrationTests.Extensions;
using JamCentral.Controllers.API;
using JamCentral.Models;
using JamCentral.Persistence;
using NUnit.Framework;
using System.Linq;
using JamCentral.Dtos;
using FluentAssertions;
using System;
using System.Web.Http.Results;

namespace JamCentral.IntegrationTests.Controllers.API
{
    [TestFixture]
    public class AttendancesControllerTests
    {
        private ApplicationDbContext _context;
        private AttendencesController _controller;
        private ApplicationUser _user;
        private Genre _genre;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            var unitOfWork = new UnitOfWork(_context);
            _controller = new AttendencesController(unitOfWork);
            _user = _context.Users.First();
            _genre = _context.Genres.First();
            _controller.MockCurrentUser(_user.Id, _user.UserName);            
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test, Isolated]
        public void Attend_CalledCorrectly_AddAnAttendance()
        {
            var gig = new Gig(_user.Id, "-", DateTime.Now.AddDays(1), _genre.Id);
            _context.Gigs.Add(gig);
            _context.SaveChanges();
            var dto = new AttendenceDto { GigId = _context.Gigs.First().Id };

            _controller.Attend(dto);

            _context.Attendences.Should().HaveCount(1);
        }

        [Test, Isolated]
        public void Attend_AttendanceExistInDb_ReturnBadRequest()
        {
            var gig = new Gig(_user.Id, "-", DateTime.Now.AddDays(1), _genre.Id);
            _context.Gigs.Add(gig);
            _context.SaveChanges();
            var dto = new AttendenceDto { GigId = _context.Gigs.First().Id };
            _controller.Attend(dto);

            var result = _controller.Attend(dto);

            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [Test, Isolated]
        public void Unbook_CalledCorrectly_DeleteTheAttendance()
        {
            var gig = new Gig(_user.Id, "-", DateTime.Now.AddDays(1), _genre.Id);
            _context.Gigs.Add(gig);
            _context.SaveChanges();
            var dto = new AttendenceDto { GigId = _context.Gigs.First().Id };
            _controller.Attend(dto);

            _controller.Unbook(dto);

            _context.Attendences.Should().HaveCount(0);
        }

        [Test, Isolated]
        public void Unbook_AttendanceNotInDb_ReturnBadRequest()
        {
            var dto = new AttendenceDto { GigId = 0 };                    

            var result = _controller.Unbook(dto);

            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }


    }
}
