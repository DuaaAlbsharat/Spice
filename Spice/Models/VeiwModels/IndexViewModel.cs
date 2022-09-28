using System.Collections.Generic;

namespace Spice.Models.VeiwModels
{
    public class IndexViewModel
    {
        public IEnumerable<MenuItem> ListMenuItems { get; set; }
        public IEnumerable<Coupon> ListCoupons { get; set; }
        public IEnumerable<Category> ListCategories { get; set; }
    }
}
