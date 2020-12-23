using JamCentral.Controllers.API;
using JamCentral.Persistence;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Mvc;
using JamCentral.Tests.Extensions;
using FluentAssertions;
using JamCentral.Repositories;
using System.Web.Http.Results;
using JamCentral.Models;

namespace JamCentral.Tests.Controllers.API
{
    [TestClass]
    public class GigsControllerTests
    {
        private GigsController _controller;
        private Mock<IGigsRepository> _mockRepository;

        public GigsControllerTests()
        {
            _mockRepository = new Mock<IGigsRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Gigs).Returns(_mockRepository.Object);

            _controller = new GigsController(mockUoW.Object);
            _controller.MockCurrentUser("1", "pepito@midominio.com");
        }

        [TestMethod]
        public void Cancel_NogigWithgivenIdExists_ReturnNotFound()
        {
            var result = _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_GigAlreadyCanceled_ReturnBadRequest()
        {
            var gig = new Gig("1","location", DateTime.Now.AddDays(1),2);

            gig.Cancel();

            _mockRepository.Setup(r => r.GetGigWithAttendanceAndFolllowers(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<BadRequestResult>();
        }

        [TestMethod]
        public void Cancel_UserUnAuthorized_ReturnUnauthorized()
        {
            var gig = new Gig("2","location", DateTime.Now.AddDays(1),2);

            _mockRepository.Setup(r => r.GetGigWithAttendanceAndFolllowers(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<UnauthorizedResult>();
        }

        [TestMethod]
        public void Cancel_ValidCancel_ReturnOk()
        {
            var gig = new Gig("1","location", DateTime.Now.AddDays(1),2);

            _mockRepository.Setup(r => r.GetGigWithAttendanceAndFolllowers(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<OkResult>();
        }
    }
}
