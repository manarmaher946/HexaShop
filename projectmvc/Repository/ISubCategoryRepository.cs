using projectmvc.Models;

namespace projectmvc.Repository
{
    public interface ISubCategoryRepository
    {
        List<SubCategory> GetAll();
        SubCategory GetById(int id);
        void Insert(SubCategory cat);
        void Update(int id, SubCategory cat);
        void Delete(int id);

        List<SubCategory> GetSubCatById(int id);
        Category GetCategory(int id);

        /*public int GetAllProductIdBySubCategoryId(int id);*/
        public List<int> ShowListOFSubCategory(int id);
    }
}
