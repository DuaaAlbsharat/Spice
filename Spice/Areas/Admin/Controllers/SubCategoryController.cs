using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Spice.Data;
using Spice.Models;
using Spice.Models.VeiwModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoryController : Controller
    {
        private readonly    ApplicationDbContext db;
        public SubCategoryController(ApplicationDbContext _db)
        {
            db=_db; 
        }
        public IActionResult Index()
        {
            var subCategory = db.SubCategories.Include(x => x.Category).ToList();
            return View(subCategory);
        }
        [HttpGet]
        public async Task<IActionResult>Create()
        {
            SubCategoryAndCategoryViewModel model = new SubCategoryAndCategoryViewModel
            {
                CategoryList = await db.Categories.ToListAsync(),
                SubCategory = new Models.SubCategory(),
                SubCategoriesList = await db.SubCategories.OrderBy(x=>x.Name).Select(x=>x.Name).Distinct().ToListAsync()    
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubCategoryAndCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ExistssubCategores = db.SubCategories.Include(m => m.Category).Where(m => m.CategoryId == (model.SubCategory.CategoryId) && (m.Name) == (model.SubCategory.Name));
                if (ExistssubCategores.Count() > 0)
                {
                   model.StatusMessage =   "Error: Thie is Sub Category Exists under " + ExistssubCategores.FirstOrDefault().Category.Name + " Category ";
                }
                //model.StatusMessage =
                else
                {
                    db.SubCategories.Add(model.SubCategory);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

            }

            SubCategoryAndCategoryViewModel modelSub = new SubCategoryAndCategoryViewModel()
            {
                CategoryList = await db.Categories.ToListAsync(),
                SubCategory = new Models.SubCategory(),
                StatusMessage = model.StatusMessage
            };
            return View(modelSub);
        }
        [HttpGet]
        public async Task<IActionResult> GetSubCategores(int id)
        {
            List<SubCategory> subCategories = new List<SubCategory>();
            subCategories = await db.SubCategories.Where(m => m.Category.Id== id).ToListAsync();
            return Json(new SelectList(subCategories, "Id", "Name"));
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var subCatg = db.SubCategories.Include(m => m.Category).Where(m => m.Id == id).SingleOrDefault();
            if (subCatg == null)
            {
                return NotFound();
            }
            return View(subCatg);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SubCategory model)
        {
            db.SubCategories.Update(model);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "SubCategory");

        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var subCatg = db.SubCategories.Include(m => m.Category).Where(m => m.Id == id).SingleOrDefault();
            if(subCatg == null)
            {
                return NotFound();
            }
            return View(subCatg);   
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(SubCategory model)
        {
            db.SubCategories.Remove(model);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "SubCategory");

        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var sub =await db.SubCategories.Include(x => x.Category).Where(x => x.Id == id).SingleOrDefaultAsync();
           return View(sub);  
        }


    }

       
    
}
