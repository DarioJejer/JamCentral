using JamCentral.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace JamCentral.Controllers
{
    public class FollowingsController : Controller
    {
        private ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Followins
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var artists = _context.Followings                
                .Where(f => f.UserId == userId)
                .Select(f => f.Artist)
                .ToList();

            return View(artists);
        }
    }
}