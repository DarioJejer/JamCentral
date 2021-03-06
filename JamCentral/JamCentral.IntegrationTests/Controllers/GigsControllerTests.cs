﻿using AutoMapper;
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
using System.Web.Mvc;

namespace JamCentral.IntegrationTests.Controllers
{
    [TestFixture]
    public class GigsControllerTests
    {
        private ApplicationDbContext _context;
        private GigsController _controller;
        private ApplicationUser _user;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new GigsController(new UnitOfWork(_context));
            _user = _context.Users.First();
            _controller.MockCurrentUser(_user.Id, _user.UserName);
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());
        }

        [TearDown]
        public void TearDown()
        {
            Mapper.Reset();
            _context.Dispose();
        }

        [Test, Isolated]
        public void Mine_CalledCorrectly_ReturnGigs()
        {
            var genre = _context.Genres.Single(g => g.Id == 1);
            var gig = new Gig(_user.Id, "-", DateTime.Now.AddDays(1), genre.Id);
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            var result = _controller.Mine();

            (result.ViewData.Model as IEnumerable<Gig>).Should().HaveCount(1);
        }

        [Test, Isolated]
        public void Update_CalledCorrectly_ModifyTheGigOnDb()
        {
            var genre = _context.Genres.Single(g => g.Id == 1);
            var gig = new Gig(_user.Id, "-", DateTime.Now.AddDays(1), genre.Id);
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

        [Test, Isolated]
        public void Edit_CalledCorrectly_ReturnGig()
        {
            var genre = _context.Genres.First();
            var gig = new Gig(_user.Id, "-", DateTime.Now.AddDays(1), genre.Id);
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            var resultView = _controller.Edit(gig.Id) as ViewResult;

            var resulForm = resultView.ViewData.Model as GigFormViewModel;
            resulForm.Id.Should().Be(gig.Id);
            resulForm.Location.Should().Be("-");
            resulForm.GenreId.Should().Be(genre.Id);
            resulForm.Date.Should().Be(gig.Date.ToString("d MMM yyyy"));
            resulForm.Time.Should().Be(gig.Date.ToString("HH:mm"));            
        }

        [Test, Isolated]
        public void MyCalendar_CalledCorrectly_ReturnListOfGigs()
        {
            var genre = _context.Genres.First();
            var gig1 = new Gig(_user.Id, "-", DateTime.Now.AddDays(1), genre.Id);
            _context.Gigs.Add(gig1);
            var attendence1 = new Attendence
            {
                GigId = gig1.Id,
                AttendeeId = _user.Id
            };
            _context.Attendences.Add(attendence1);
            _context.SaveChanges();
            var gig2 = new Gig(_user.Id, "-", DateTime.Now.AddDays(2), genre.Id);
            _context.Gigs.Add(gig2);
            var attendence2 = new Attendence
            {
                GigId = gig2.Id,
                AttendeeId = _user.Id
            };
            _context.Attendences.Add(attendence2);
            _context.SaveChanges();

            var result = _controller.MyCalendar() as ViewResult;

            var viewModel = result.ViewData.Model as GigsViewModel;
            var list = viewModel.upcomingGigs;
            list.Should().HaveCount(2);
        }

        [Test, Isolated]
        public void Create_CalledCorrectly_CreateAGig()
        {
            var genre = _context.Genres.First();
            var viewModel = new GigFormViewModel
            {
                Id = 0, 
                Location = "-",
                Date = DateTime.Today.AddDays(1).ToString("d MMM yyyy"),
                Time = DateTime.Today.AddHours(18).ToString("HH:mm"),
                GenreId = genre.Id
            };

            _controller.Create(viewModel);

            var gig = _context.Gigs.First();
            gig.Location.Should().Be("-");
            gig.Date.Should().Be(DateTime.Today.AddDays(1).AddHours(18));
            gig.GenreId.Should().Be(genre.Id);
        }

        [Test, Isolated]
        public void Create_CalledWithFollowers_NotifyFollowers()
        {
            var genre = _context.Genres.First();
            var viewModel = new GigFormViewModel
            {
                Location = "-",
                Date = DateTime.Today.AddDays(1).ToString("d MMM yyyy"),
                Time = DateTime.Today.AddHours(18).ToString("HH:mm"),
                GenreId = genre.Id
            };
            var following = new Following { ArtistId = _user.Id, UserId = _user.Id };
            _context.Followings.Add(following);
            _context.SaveChanges();

            _controller.Create(viewModel);

            var gig = _context.Gigs.First();
            var notfication = _context.Notifications.First();
            notfication.GigId.Should().Be(gig.Id);
            var userNotification = _context.UserNotifications.First();
            userNotification.UserId.Should().Be(_user.Id);
            userNotification.NotificationId.Should().Be(notfication.Id);
            userNotification.BeenRead.Should().Be(false);
        }
    }
}
