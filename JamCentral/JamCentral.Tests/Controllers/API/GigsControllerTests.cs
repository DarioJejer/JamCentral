using JamCentral.Controllers;
using JamCentral.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Mvc;

namespace JamCentral.Tests.Controllers.API
{
    [TestClass]
    public class GigsControllerTests
    {
        public GigsControllerTests()
        {
            var identity = new GenericIdentity("pepito@midominio.com");
            identity.AddClaim(
                new System.Security.Claims.Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "pepito@midominio.com"));
            identity.AddClaim(
                new System.Security.Claims.Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "1"));
            var principal = new GenericPrincipal(identity, null);

            var mockUoW = new Mock<IUnitOfWork>();
            var controller = new GigsController(mockUoW.Object);

            controller.ControllerContext.HttpContext.User = principal;
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
