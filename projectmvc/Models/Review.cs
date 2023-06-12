using System.ComponentModel.DataAnnotations.Schema;

namespace projectmvc.Models
{
    public class Review
    {
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }

        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual  Product Product { get; set; } 
    }
}
