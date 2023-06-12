using Microsoft.EntityFrameworkCore;
using projectmvc.Models;
using projectmvc.ViewModel;

namespace projectmvc.Repository
{
    public class ProductSizeColorRepository : IProductSizeColorRepository
    {

        Context context = new Context();
        public ProductSizeColorRepository() { }
        public ProductSizeColorRepository(Context context)//inject
        {
            this.context = context;// new Context();
        }

        //CRUD operation
        public List<ProductSizeColor> GetAll()
        {
            return context.ProductSizeColor.ToList();
        }

        public ProductSizeColor GetById(int id)
        {
            return context.ProductSizeColor.FirstOrDefault(e => e.ID == id);
        }
        public void Insert(ProductSizeColor p)
        {
            ProductSizeColor productSizeColor = context.ProductSizeColor
                .FirstOrDefault(e => e.ProductID == p.ProductID && e.ProductColorID == p.ProductColorID && e.ProductSizeID == p.ProductSizeID);
            if(productSizeColor == null)
            {
            context.ProductSizeColor.Add(p);
            }
            else
            {
                productSizeColor.Quantity += p.Quantity;
            }
            context.SaveChanges();
        }

        public void Update(int id, ProductSizeColor p)
        {
            ProductSizeColor oldP = GetById(id);

            oldP.ProductID = p.ProductID;
            oldP.ProductColorID = p.ProductColorID;
            oldP.ProductSizeID = p.ProductSizeID;
            oldP.Quantity = p.Quantity;

            context.SaveChanges();

        }

        public void Delete(int id)
        {
            ProductSizeColor oldP = GetById(id);
            context.ProductSizeColor.Remove(oldP);
            context.SaveChanges();
        }

        /* public List<SubCategory> GetSubCategoryByID(int catId)
         {
             return context.SubCategory.Where(e => e.CategoryId == catId).ToList();
         }*/



        public ProductSizeColorViewModel GetProductSizeColor(int id)
        {
            ProductSizeColorViewModel PSCVM = new ProductSizeColorViewModel();
            PSCVM.Product = context.Products.FirstOrDefault(p => p.Id == id);
            List<int> ColorsID = context.ProductSizeColor.Where(p => p.ProductID == id).Select(p => p.ProductColorID).ToList();
            List<int> SizesID = context.ProductSizeColor.Where(p => p.ProductID == id).Select(p => p.ProductSizeID).ToList();
            foreach (int color in ColorsID)
            {
                PSCVM.Colors.Add(context.Color.FirstOrDefault(c => c.ID == color));
            }
            foreach (int size in SizesID)
            {
                PSCVM.Sizes.Add(context.Size.FirstOrDefault(c => c.ID == size));

            }
            return PSCVM;
        }

        public List<Product> GetLists(int item)
        {
            return context.Products.Where(i => i.SubCategoryId == item).ToList();
        }

    }
}
