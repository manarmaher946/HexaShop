using System.ComponentModel.DataAnnotations.Schema;

namespace projectmvc.Models
{
    public class SubCategory
    {

        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual  List<Product>? Products { get; set; }            
        public virtual Category? Category { get; set; }
    }
}
