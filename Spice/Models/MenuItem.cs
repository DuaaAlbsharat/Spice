using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spice.Models
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Descreption { get; set; }
        public string Spicyness { get; set; }
        [EnumDataType(typeof(Espicy))]
        public Espicy Espicy { get; set; }
        public string Image  { get; set; }
        [Display(Name="Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        [Display(Name = "SubCategory")]
        public int SubCategoryId { get; set; }
        [ForeignKey("SubCategoryId")]
        public virtual SubCategory SubCategory { get; set; }    
        [Range(0, int.MaxValue,ErrorMessage ="Price Should Be Greater Than ${1}")]    
        public double Price { get; set; }
    }
    public enum Espicy
    {
        NA, NotEspicy, Espicy, VeryEspicy
    }
}
