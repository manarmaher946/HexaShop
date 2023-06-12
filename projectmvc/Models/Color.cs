namespace projectmvc.Models
{
    public class Color
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual List<ProductSizeColor> ProductSizeColors { get; set; }

    }
}
