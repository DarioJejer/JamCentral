using JamCentral.IntegrationTests.Extensions;
using JamCentral.Controllers.API;
using JamCentral.Models;
using JamCentral.Persistence;
using NUnit.Framework;
using System.Linq;

namespace JamCentral.IntegrationTests.Controllers.API
{
    [TestFixture]
    public class AttendancesControllerTests
    {
        private ApplicationDbContext _context;
        private AttendencesController _controller;
        private ApplicationUser _user;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            var unitOfWork = new UnitOfWork(_context);
            _controller = new AttendencesController(unitOfWork);
            _user = _context.Users.First();
            _controller.MockCurrentUser(_user.Id, _user.UserName);
        }

        [Test]
        public void TestMethod1()
        {
        }
    }
}
