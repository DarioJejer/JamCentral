using JamCentral.Models;
using JamCentral.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections;
using System.Data.Entity;
using JamCentral.Tests.Extensions;
using FluentAssertions;
using System.Collections.Generic;

namespace JamCentral.Tests.Persistance.Repositories
{
    [TestClass]
    public class GigsRepositoriyTest
    {
        private GigsRepository _repository;
        private Mock<DbSet<Gig>> _mockGigs;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockGigs = new Mock<DbSet<Gig>>();
            var _mockContext = new Mock<IApplicationDbContext>();
            _mockContext.Setup(c => c.Gigs).Returns(_mockGigs.Object);

            _repository = new GigsRepository(_mockContext.Object);
        }

        [TestMethod]
        public void GetGigsOfArtist_UnExistingArtist_ReturnEmpty()
        {
            var gig = new Gig("user1", "location", DateTime.Now.AddDays(1), 2);
            _mockGigs.PopulateGigs(new[] { gig });

            var gigs = _repository.GetGigsOfArtist("user2");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetGigsOfArtist_GigIsInThePast_ReturnEmpty()
        {
            var gig = new Gig("user1", "location", DateTime.Now.AddDays(-1), 2);
            _mockGigs.PopulateGigs(new[] { gig });

            var gigs = _repository.GetGigsOfArtist("user1");

            gigs.Should().BeEmpty();
        }
        
    }
}
