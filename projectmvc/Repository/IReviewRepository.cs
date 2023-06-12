using projectmvc.Models;

namespace projectmvc.Repository
{
    public interface IReviewRepository
    {
        List<Review> GetAll();
        Review GetById(int id);
        void Insert(Review r);
        void Update(int id, Review r);
        void Delete(int id);
    }
}