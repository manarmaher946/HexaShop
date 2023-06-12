using projectmvc.Models;

namespace projectmvc.Repository
{
    public class CategoryRepository: ICategoryRepository
    {
        Context context = new Context();
        public CategoryRepository() { }
        public CategoryRepository(Context context)//inject
        {
            this.context = context;// new Context();
        }

        //CRUD operation
        public List<Category> GetAll()
        {
            return context.Category.ToList();
        }

        public Category GetById(int id)
        {
            return context.Category.FirstOrDefault(e => e.Id == id);
        }
        public void Insert(Category cat)
        {
            context.Category.Add(cat);
            context.SaveChanges();
        }

        public void Update(int id, Category cat)
        {
            Category oldCat = GetById(id);

            oldCat.Name = cat.Name;
            

            context.SaveChanges();

        }

        public void Delete(int id)
        {
            Category oldCat = GetById(id);
            context.Category.Remove(oldCat);
            context.SaveChanges();
        }

        public List<SubCategory> GetSubCategoryByID(int catId)
        {
            return context.SubCategory.Where(e => e.CategoryId == catId ).ToList();
        }



    }
}
