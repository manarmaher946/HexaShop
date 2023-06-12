using Microsoft.EntityFrameworkCore;
using projectmvc.Models;

namespace projectmvc.Repository
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        Context context = new Context();
        public SubCategoryRepository() { }
        public SubCategoryRepository(Context context)//inject
        {
            this.context = context;// new Context();
        }

        public List<SubCategory> GetAll()
        {
            return context.SubCategory.ToList();
        }

        public SubCategory GetById(int id)
        {
            return context.SubCategory.FirstOrDefault(e => e.Id == id);
        }

        public List<SubCategory> GetSubCatById(int id)
        {
            return context.SubCategory.Where(e => e.CategoryId == id).ToList();
        }
        public void Insert(SubCategory subCat)
        {
            context.SubCategory.Add(subCat);
            context.SaveChanges();
        }

        public void Update(int id, SubCategory subCat)
        {
            SubCategory oldSubCat = GetById(id);

            oldSubCat.Name = subCat.Name;
            oldSubCat.CategoryId = subCat.CategoryId;
            //foreach(Product item  in oldSubCat.Products)
            //{
            //    item.Name = subCat.Name;
            //    item.Price = subCat.Products.
            //}
            oldSubCat.Products = subCat.Products;

            context.SaveChanges();

        }

        public void Delete(int id)
        {
            SubCategory oldSubCat = GetById(id);
            context.SubCategory.Remove(oldSubCat);
            context.SaveChanges();
        }

        public Category GetCategory(int id)
        {
            var subCat = context.SubCategory.Include(e => e.Category).FirstOrDefault(c => c.CategoryId == c.Category.Id);
            return context.Category.Where(e => e.Id == subCat.Id).FirstOrDefault();
        }

       /* public int GetAllProductIdBySubCategoryId(int id)
        {
            var subCat = GetById(id);
            //var x =  context.ProductSizeColor.Include(e => e.Product).Where(e=>e.SubCategoryId== subCat.Id && e.ProductID==e.Product.Id).ToList();

            //var x = context.ProductSizeColor.FirstOrDefault(e => e.SubCategoryId == subCat.Id);
            return context.Products.FirstOrDefault(e => e.SubCategoryId == subCat.Id).ProductID;
            //return context.SubCategory.Where(e => e.Products.Select(p=>p.ProductID).FirstOrDefault() == id).ToList();
        }*/



        public List<int> ShowListOFSubCategory(int id)
        {

            return context.SubCategory.Where(i => i.CategoryId == id).Select(e => e.Id).ToList();
        }
    }
}
