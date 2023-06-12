using projectmvc.Models;

namespace projectmvc.Repository
{
    public interface IColorRepository
    {


        List<Color> GetAll();
        Color GetById(int id);
        void Insert(Color c);
        void Update(int id, Color c);
        void Delete(int id);
    }
}