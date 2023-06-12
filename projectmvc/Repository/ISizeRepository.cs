using projectmvc.Models;

namespace projectmvc.Repository
{
    public interface ISizeRepository
    {
        List<Size> GetAll();
        Size GetById(int id);
        void Insert(Size sz);
        void Update(int id, Size sz);
        void Delete(int id);
    }
}