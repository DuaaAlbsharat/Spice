using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spice.Data;
using Spice.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Spice.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext db;
        public UsersController(ApplicationDbContext _db)
        {
            db = _db;
        }
        public async Task< IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            return View(await db.ApplicationUsers.Where(x=>x.Id != claim.Value).ToListAsync());
        }
    }
}
