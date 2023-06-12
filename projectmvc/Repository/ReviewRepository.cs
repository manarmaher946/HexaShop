using projectmvc.Models;

namespace projectmvc.Repository
{
    public class ReviewRepository: IReviewRepository
    {

        Context context = new Context();
        public ReviewRepository() { }
        public ReviewRepository(Context context)//inject
        {
            this.context = context;// new Context();
        }

        //CRUD operation
        public List<Review> GetAll()
        {
            return context.Review.ToList();
        }

        public Review GetById(int id)
        {
            return context.Review.FirstOrDefault(e => e.Id == id);
        }
        public void Insert(Review R)
        {
            context.Review.Add(R);
            context.SaveChanges();
        }

          public void Update(int id, Review R)
           {
               Review oldR = GetById(id);

            oldR.ProductID = R.ProductID;
            oldR.CustomerID= R.CustomerID;

               context.SaveChanges();

           }

        public void Delete(int id)
        {
            Review R = GetById(id);
            context.Review.Remove(R);
            context.SaveChanges();
        }
    }
}
