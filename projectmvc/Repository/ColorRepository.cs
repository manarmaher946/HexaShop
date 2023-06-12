using projectmvc.Models;

namespace projectmvc.Repository
{
    public class ColorRepository : IColorRepository
    {

        Context context =new Context();
        public ColorRepository() { }
        public ColorRepository(Context context)//inject
        {
            this.context = context;// new Context();
        }

        //CRUD operation
        public List<Color> GetAll()
        {
            return context.Color.ToList();
        }

        public Color GetById(int id)
        {
            return context.Color.FirstOrDefault(e => e.ID == id);
        }
        public void Insert(Color c)
        {
            context.Color.Add(c);
            context.SaveChanges();
        }

        public void Update(int id, Color c)
        {
            Color oldc = GetById(id);

            oldc.Name = oldc.Name;
            context.SaveChanges();

        }

        public void Delete(int id)
        {
            Color oldc = GetById(id);
            context.Color .Remove(oldc);
            context.SaveChanges();
        }


    }
}
