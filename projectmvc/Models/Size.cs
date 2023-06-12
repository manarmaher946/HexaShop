namespace projectmvc.Models
{
    public class Size
    {
        public int ID { get; set; }
        public string size { get; set; }
        public virtual List<ProductSizeColor> ProductSizeColors { get; set; }


    }
}
