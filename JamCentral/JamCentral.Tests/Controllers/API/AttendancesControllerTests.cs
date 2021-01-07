using FluentAssertions;
using JamCentral.Controllers.API;
using JamCentral.Dtos;
using JamCentral.Persistence;
using JamCentral.Repositories;
using JamCentral.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace JamCentral.Tests.Controllers.API
{
    [TestClass]
    public class AttendancesControllerTests
    {
        private Mock<IAttendencesRepository> _mockRepository;
        private AttendencesController _controller;
        private int gigId = 1;
        private string userId = "1";

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IAttendencesRepository>();
             var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.Attendences).Returns(_mockRepository.Object);
            _controller = new AttendencesController(mockUnitOfWork.Object);
            _controller.MockCurrentUser(userId, "pepito@midominio.com");
        }

        [TestMethod]
        public void Attend_TheAttendanceAlreadyExist_ReturnBadRequest()
        {
            _mockRepository.Setup(r => r.GetAttendenceExistInDb(userId, gigId)).Returns(true);
            var dto = new AttendenceDto { GigId = gigId };

            var result = _controller.Attend(dto);

            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }
    }
}
