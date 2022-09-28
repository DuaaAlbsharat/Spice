using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Spice.Data;
using Spice.Models;
using Spice.Models.VeiwModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext db;
        public HomeController(ApplicationDbContext _db)
        {
            db = _db; 
        }

        public async Task<IActionResult> Index()
        {
            IndexViewModel indexViewModel = new IndexViewModel()
            {
                ListCategories = await db.Categories.ToListAsync(),
                ListCoupons = await db.Coupons.Where(x => x.IsActive == true).ToListAsync(),
                ListMenuItems = await db.MenuItems.Include(m => m.Category).Include(m => m.SubCategory).ToListAsync()
            };
            return View(indexViewModel);
        }
    }
}
