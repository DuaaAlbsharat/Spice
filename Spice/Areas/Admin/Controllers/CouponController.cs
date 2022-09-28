using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Spice.Data;
using Spice.Models;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Spice.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CouponController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IHostingEnvironment  hostingEnvironment;
        public CouponController(ApplicationDbContext _db , IHostingEnvironment  _hostingEnvironment)
        {
                db= _db;
                hostingEnvironment= _hostingEnvironment;    
        }
        public IActionResult Index()
        {
            var coupon = db.Coupons.ToList();
            return View(coupon);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Coupon coupon)
        {
            if (ModelState.IsValid)
            {
                var myFile = HttpContext.Request.Form.Files;
                foreach (var Image in myFile)
                {

                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;
                        var path = Path.Combine(hostingEnvironment.WebRootPath, "CouponImages");
                        if (file.Length > 0)
                        {
                            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                            using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                coupon.Picture = "~/CouponImages/" + fileName;
                            }
                        }
                    }
                }
                db.Coupons.Add(coupon);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Coupon");
            }
            return View(coupon);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var CouponId = await db.Coupons.FindAsync(id);

            if (CouponId == null)
            {
                return NotFound();
            }
            return View(CouponId);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Coupon coupon)
        {
            var CouponsToUpdate = await db.Coupons.AsNoTracking().Where(e => e.CouponId.Equals(coupon.CouponId)).FirstOrDefaultAsync();
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    var myFile = HttpContext.Request.Form.Files;
                    foreach (var Image in myFile)
                    {

                        if (Image != null && Image.Length > 0)
                        {
                            var file = Image;
                            var path = Path.Combine(hostingEnvironment.WebRootPath, "CouponImages");
                            if (file.Length > 0)
                            {
                                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                                using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                                {
                                    await file.CopyToAsync(fileStream);
                                    coupon.Picture = "~/CouponImages/" + fileName;
                                }
                            }
                        }
                    }
                    if (CouponsToUpdate != null)
                    {
                        CouponsToUpdate.CouponType = coupon.CouponType;
                        CouponsToUpdate.ECouponType = coupon.ECouponType;
                        CouponsToUpdate.Name = coupon.Name;
                        CouponsToUpdate.MinAmount = coupon.MinAmount;
                        CouponsToUpdate.Picture = coupon.Picture;
                        CouponsToUpdate.Discount = coupon.Discount;
                        CouponsToUpdate.IsActive = coupon.IsActive;
                        db.Coupons.Update(CouponsToUpdate);
                    };
                }
                db.Coupons.Update(CouponsToUpdate);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Coupon");
            }
            return View(coupon);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var coupon = await db.Coupons.FindAsync(id);
            if (coupon == null)
            {
                return NotFound();
            }
            return View(coupon);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var couponId = await db.Coupons.FindAsync(id);
            if (couponId == null)
            {
                return NotFound();
            }
            return View(couponId);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coupones = await db.Coupons.FindAsync(id);
            db.Coupons.Remove(coupones);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Coupon");
        }
        private bool CouponExists(int id)
        {
            return db.Coupons.Any(e => e.CouponId == id);
        }
    }
}

