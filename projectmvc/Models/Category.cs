using System.ComponentModel.DataAnnotations;

namespace projectmvc.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

        public virtual List<SubCategory>? SubCategories { get; set; }
    }
}
