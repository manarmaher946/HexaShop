using System.ComponentModel.DataAnnotations.Schema;

namespace projectmvc.Models
{
    public class OrderItems
    {
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int ProductID { get; set; }

        [ForeignKey("Orders")]
        public int OrderID { get; set; }
        public int Quentity { get; set; }

        public virtual Orders Orders { get; set; }
        public virtual ProductSizeColor Product { get; set; }

    }
}
