using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectmvc.Models
{

    public class Orders
    {
        public int Id { get; set; }

        public double TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? ArrivedDate { get; set; }

        public string Address { get; set; }

        public double Discount { get; set; }

        [ForeignKey("Customer")]
        public string CustomerID{ get; set; }

        [ForeignKey("OrderStatus")]
        public int OrderStatusID { get; set; }

        [ForeignKey("Shipper")]
        
        public string? ShipperID { get; set; }


        public virtual Shipper? Shipper { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual ApplicationUser Customer{ get; set; }  
    }
}
