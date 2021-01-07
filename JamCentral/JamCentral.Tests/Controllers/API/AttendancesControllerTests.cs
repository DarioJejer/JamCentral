using JamCentral.Controllers.API;
using JamCentral.Persistence;
using JamCentral.Repositories;
using JamCentral.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace JamCentral.Tests.Controllers.API
{
    [TestClass]
    public class AttendancesControllerTests
    {
        private Mock<IAttendencesRepository> _mockRepository;
        private AttendencesController _controller;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IAttendencesRepository>();
             var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.Attendences).Returns(_mockRepository.Object);
            _controller = new AttendencesController(mockUnitOfWork.Object);
            _controller.MockCurrentUser("1", "pepito@midominio.com");
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
