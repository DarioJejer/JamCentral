using JamCentral.Dtos;
using JamCentral.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JamCentral.Controllers.API
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userInDb = User.Identity.GetUserId();
            var existInDb = _context.Followings.Any(f => f.ArtistId == dto.ArtistId && f.UserId == userInDb);

            if (existInDb)
                return BadRequest("Already following this Artist");

            var following = new Following
            {
                ArtistId = dto.ArtistId,
                UserId = userInDb
            };

            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok();
        }
    }
}
