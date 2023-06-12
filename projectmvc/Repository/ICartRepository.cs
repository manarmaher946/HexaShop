using projectmvc.Models;

namespace projectmvc.Repository
{
    public interface ICartRepository
    {

        List<Cart> GetAll(string id);
        Cart GetById(int id);
        void Insert(Cart cat);
        //void Update(int id, Cart cat);
        void Delete(int id);
    }
}