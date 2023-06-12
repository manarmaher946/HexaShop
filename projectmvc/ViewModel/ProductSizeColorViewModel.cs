using projectmvc.Models;

namespace projectmvc.ViewModel
{
    public class ProductSizeColorViewModel
    {
        public Product Product { get; set; }
        public HashSet<Color> Colors { get; set; } = new HashSet<Color>();
        public HashSet<Size> Sizes { get; set; } = new HashSet<Size>();

    }
}
