using System.Collections.Generic;

namespace Spice.Models.VeiwModels
{
    public class SubCategoryAndCategoryViewModel
    {
        public  IEnumerable<Category> CategoryList { get; set; }
        public SubCategory SubCategory { get; set; }        
        public List<string> SubCategoriesList { get; set; }
        public string StatusMessage { get; set; }
    }
}
