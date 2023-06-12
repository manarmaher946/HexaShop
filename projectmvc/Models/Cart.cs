using System.ComponentModel.DataAnnotations.Schema;

namespace projectmvc.Models
{
    public class Cart
    {
        public int Id { get; set; }
        [ForeignKey("Customer")]
        public string CustomerID { get; set; }
        [ForeignKey("ProductSizeColor")]
        public int ProductID { get; set; }
        public int Count { get; set; }

        public virtual ProductSizeColor ProductSizeColor { get; set; }
        public virtual ApplicationUser Customer { get; set; }
    }
}
