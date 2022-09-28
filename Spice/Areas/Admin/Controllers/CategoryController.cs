using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spice.Data;
using Spice.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext db;
        public CategoryController(ApplicationDbContext _db)
        {
            db=_db; 
        }
        
        public async Task< IActionResult> Index()
        {
            return View(await db.Categories.ToListAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(category);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
               return NotFound();  
            }
            var category = await db.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
           
            if (ModelState.IsValid)
            {
                db.Categories.Update(category);
                await  db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(category);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await db.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost , ActionName("Delete")]
        public async Task <IActionResult> DeleteConfermed(int? id)
        {
            var categoryId = await db.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if(categoryId == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                db.Categories.Remove(categoryId);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryId);
        }
        [HttpGet]
        public async Task <IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await db.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

    }
}
