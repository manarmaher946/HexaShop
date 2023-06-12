using projectmvc.Models;
using projectmvc.ViewModel;

namespace projectmvc.Repository
{
    public interface IProductSizeColorRepository
    {

        List<ProductSizeColor> GetAll();
        ProductSizeColor GetById(int id);
        void Insert(ProductSizeColor cat);
        void Update(int id, ProductSizeColor cat);
        void Delete(int id);
        public List<Product> GetLists(int item);
        ProductSizeColorViewModel GetProductSizeColor(int id);
    }
}