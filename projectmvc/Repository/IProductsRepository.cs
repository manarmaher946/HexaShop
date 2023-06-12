using projectmvc.Models;

namespace projectmvc.Repository
{
    public interface IProductsRepository
    {

        List<Product> GetAll();
        Product GetById(int id);
        void Insert(Product P);
        void Update(int id, Product p);
        void Delete(int id);
        List<Size> sizes(int id);
        List<Product> GetBySupplierId(string ID);
        public List<Color> Colors(int id);
    }
}