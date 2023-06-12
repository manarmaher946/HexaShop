using projectmvc.Models;

namespace projectmvc.Repository
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category GetById(int id);
        void Insert(Category cat);
        void Update(int id, Category cat);
        void Delete(int id);
        List<SubCategory> GetSubCategoryByID(int catId);
    }
}
