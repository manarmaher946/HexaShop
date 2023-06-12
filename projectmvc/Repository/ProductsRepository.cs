using projectmvc.Models;

namespace projectmvc.Repository
{
    public class ProductsRepository :IProductsRepository
    {
        Context context = new Context();
        public ProductsRepository() { }
        public ProductsRepository(Context context)//inject
        {
            this.context = context;
        }

        //CRUD operation
        public List<Product> GetAll()
        {
            return context.Products.ToList();
        }

        public Product GetById(int id)
        {
            return context.Products.FirstOrDefault(e => e.Id == id);
        }
        public List<Color> Colors(int id)
        {
            return context.ProductSizeColor.Where(p => p.ProductID == id).Select(p => p.Color).ToList();

        }
        public void Insert(Product p)
        {
            context.Products.Add(p);
            context.SaveChanges();
        }
        public List<Size> sizes(int id)
        {
           return context.ProductSizeColor.Where(p => p.ProductID == id).Select(p => p.Size).ToList();
        }

        public void Update(int id, Product newP)
        {
            Product oldP = GetById(id);

            oldP.Name = newP.Name;
            oldP.Description = newP.Description;
            oldP.Price = newP.Price;
            oldP.Image = newP.Image;
            oldP.Rating= newP.Rating;
            oldP.SupplierID= newP.SupplierID;

             context.SaveChanges();

        }

        public void Delete(int id)
        {
            Product oldP = GetById(id);
            context.Products.Remove(oldP);
            context.SaveChanges();
        }



      public List<Product> GetBySupplierId(string ID)
        {

            return context.Products.Where(s=>s.SupplierID == ID).ToList();   
        }
        /* public List<SubCategory> GetSubCategoryByID(int catId)
         {
             return context.SubCategory.Where(e => e.CategoryId == catId).ToList();
         }*/




    }
}
