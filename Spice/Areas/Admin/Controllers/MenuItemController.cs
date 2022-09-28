using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Spice.Data;
using Spice.Models;
using Spice.Models.VeiwModels;
using System;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Spice.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuItemController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IHostEnvironment hostEnvironment;
        [BindProperty]
        public MenuItemViewModel MenuItemVM { get; set; }
        public MenuItemController(ApplicationDbContext _db , IHostEnvironment _hostEnvironment )
        {
            db = _db;
            hostEnvironment = _hostEnvironment;
            MenuItemVM = new MenuItemViewModel
            {
                Categories = db.Categories,
                MenuItem = new Models.MenuItem()
            };
        }
        public async Task< IActionResult> Index()
        {
            var menuItem = await db.MenuItems.Include(x => x.Category).Include(x => x.SubCategory).ToListAsync();
            return View(menuItem);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(MenuItemVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuItemViewModel model)
        {
            MenuItemViewModel MenuVM = new MenuItemViewModel
            {
                MenuItem = model.MenuItem,
                Categories = await db.Categories.ToListAsync()
            };
            if (ModelState.IsValid)
            {
                var myFile = HttpContext.Request.Form.Files;
                foreach (var Image in myFile)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;
                        var upload = Path.Combine(hostEnvironment.ContentRootPath + "\\wwwroot\\MenuImages\\");
                        if (file.Length > 0)
                        {
                            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                            using (var fileStream = new FileStream(Path.Combine(upload, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                MenuVM.MenuItem.Image = "~/MenuImages/" + fileName;
                            }
                        }
                    }
                }
                db.MenuItems.Add(MenuVM.MenuItem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "MenuItem");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();  
            }
            var menuId = await db.MenuItems.Include(x => x.Category).Include(x => x.SubCategory).Where(x => x.Id == id).SingleOrDefaultAsync();
            if(menuId == null)
            {
                return NotFound();
            }
            var Item = new MenuItemViewModel
            {
                MenuItem = new MenuItem(),
                Categories = await db.Categories.ToListAsync(),
            };
            Item.MenuItem = menuId;
            Item.SubCategories = await db.SubCategories.Where(m => m.CategoryId == Item.MenuItem.CategoryId).ToListAsync();
            return View(Item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MenuItemViewModel model)
        {
            MenuItemViewModel MenuVM = new MenuItemViewModel
            {
                MenuItem = model.MenuItem,
                Categories = await db.Categories.ToListAsync()
            };

            if (ModelState.IsValid)
            {
                var myFile = HttpContext.Request.Form.Files;
                foreach (var Image in myFile)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;
                        var path = Path.Combine(hostEnvironment.ContentRootPath + "\\wwwroot\\MenuImages\\");
                        if (file.Length > 0)
                        {
                            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                            using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                MenuVM.MenuItem.Image = "~/MenuImages/" + fileName;
                            }
                        }
                    }
                }
                db.MenuItems.Update(MenuVM.MenuItem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "MenuItem");
            }
            return View(model);
        }
        [HttpGet]
        public async Task <IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var menuId = await db.MenuItems.Include(x => x.Category).Include(x => x.SubCategory).Where(x => x.Id == id).SingleOrDefaultAsync();
            if (menuId == null)
            {
                return NotFound();
            }
            
            return View(menuId);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var menuId = await db.MenuItems.Include(x => x.Category).Include(x => x.SubCategory).Where(x => x.Id == id).SingleOrDefaultAsync();
            if (menuId == null)
            {
                return NotFound();
            }
            return View(menuId);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(MenuItem model)
        {
            
            db.MenuItems.Remove(model);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "MenuItem");

        }
    }

}
