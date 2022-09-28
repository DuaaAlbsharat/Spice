using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Spice.Models
{
    public class Coupon
    {
        public int CouponId { get; set; }
        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter Coupon Type")]
        [Display(Name = "Coupon Type")]
        public string CouponType { get; set; }
        [Required(ErrorMessage = "ECoupon Type")]
        [Display(Name = "ECoupon Type")]
        [EnumDataType(typeof(ECouponType))]
        public ECouponType ECouponType { get; set; }
        [Required(ErrorMessage = "plaese Enter  Discount")]
        public double Discount { get; set; }
        [Required(ErrorMessage = "plaese Enter  MinAmount")]
        [Display(Name = "Min Amount")]
        public double MinAmount { get; set; }
        public string Picture { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

    }
    public enum ECouponType
    {
        Percent, Doller
    }
}
