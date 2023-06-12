using Microsoft.EntityFrameworkCore;
using projectmvc.Models;

namespace projectmvc.Repository
{
    public class CartRepository: ICartRepository
    {

        Context context =new Context();
        public CartRepository() { }
        public CartRepository(Context context)//inject
        {
            this.context = context;// new Context();
        }

        //CRUD operation
        public List<Cart> GetAll(string id)
        {
            return context.Cart.Where(c => c.CustomerID == id).Include(p => p.ProductSizeColor).ThenInclude(p => p.Product).Include(p => p.ProductSizeColor).ThenInclude(p => p.Color).Include(p => p.ProductSizeColor).ThenInclude(p => p.Size).ToList();
        }

        public Cart GetById(int id)
        {
            return context.Cart.FirstOrDefault(e => e.Id == id);
        }
        public void Insert(Cart cart)
        {
            context.Cart.Add(cart);
            context.SaveChanges();
        }

     /*   public void Update(int id, Cart cart)
        {
            Cart oldCart = GetById(id);

            oldCart.ProductID = cart.ProductID;
            oldCart.CustomerID= cart.CustomerID;

            context.SaveChanges();

        }*/

        public void Delete(int id)
        {
            Cart oldCart = GetById(id);
            context.Cart.Remove(oldCart);
            context.SaveChanges();
        }


    }
}
