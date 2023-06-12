using projectmvc.Models;

namespace projectmvc.Repository
{
    public class SizeRepository : ISizeRepository
    {

        Context context = new Context();
        public SizeRepository() { }
        public SizeRepository(Context context)//inject
        {
            this.context = context;// new Context();
        }

        //CRUD operation
        public List<Size> GetAll()
        {
            return context.Size.ToList();
        }

        public Size GetById(int id)
        {
            return context.Size.FirstOrDefault(e => e.ID == id);
        }
        public void Insert(Size sz)
        {
            context.Size.Add(sz);
            context.SaveChanges();
        }

        public void Update(int id, Size sz)
        {
            Size oldsz = GetById(id);

            oldsz.size = sz.size;
              context.SaveChanges();

        }

        public void Delete(int id)
        {
            Size oldsz = GetById(id);
            context.Size.Remove(oldsz);
            context.SaveChanges();
        }



    }

   
}
