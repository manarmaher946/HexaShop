using System.ComponentModel.DataAnnotations.Schema;

namespace projectmvc.Models
{
    public class ProductSizeColor
    {

        public int ID { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }


        [ForeignKey("Color")]
        public int ProductColorID { get; set; }
        
        [ForeignKey("Size")]
        public int ProductSizeID { get; set; }

        public int Quantity { get; set; }



        public virtual Product Product { get; set; }
        public virtual Color Color { get; set; }
        public virtual Size Size { get; set; }

    }

}
