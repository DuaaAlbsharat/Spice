using System.Collections.Generic;

namespace Spice.Models.VeiwModels
{
    public class MenuItemViewModel
    {
        public MenuItem MenuItem { get; set; }
        public IEnumerable <Category> Categories { get; set; }
        public IEnumerable<SubCategory>SubCategories { get; set; }
    }
}
