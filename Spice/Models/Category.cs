using System.ComponentModel.DataAnnotations;

namespace Spice.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Enter Name Category")]
        [Display(Name ="Category Name")]
        public string Name { get; set; }
        
    }
}
