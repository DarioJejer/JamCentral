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

namespace JamCentral.Tests.Controllers.API
{
    [TestClass]
    public class GigsControllerTests
    {
        private GigsController _controller;
        public GigsControllerTests()
        {
            

            var mockUoW = new Mock<IUnitOfWork>();
            _controller = new GigsController(mockUoW.Object);

            _controller.MockCurrentUser("1", "pepito@midominio.com");
        }

        [TestMethod]
        public void TestMethod1()
        {

        }
    }
}
