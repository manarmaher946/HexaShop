using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.Documents;
using Microsoft.EntityFrameworkCore;
using projectmvc.Models;
using projectmvc.Repository;
using projectmvc.ViewModel;
using System.Security.Cryptography;

namespace projectmvc.Hubs
{
    public class AddCommentHub:Hub
    {
        Context context = new Context();
     /*
        public override Task OnConnectedAsync()
        {
            string name = "Anonymus";

            Clients.All.SendAsync("NewUser", name, Context.ConnectionId);
            return base.OnConnectedAsync();
        }*/

        public void AddCMNT(int Pid, string Cid, String cmnt,
            int rating,string Cname)
        {
            Review review = new Review()
            {
                ProductID = Pid,
                CustomerID = Cid,
                CustomerName= Cname,
                Rating = rating,
                Comment = cmnt
            };

            //ReviewRepository.Insert(review);


            context.Review.Add(review);
            context.SaveChanges();


            Clients.All.SendAsync("CmntAdded", Pid,cmnt, Cname);

        }


        public void AddToCard(int size, int color , int ProductID, string UserID)
        {
          
            ProductSizeColor psc = context.ProductSizeColor.Include(p => p.Product).Where(p => p.ProductSizeID == size && p.ProductID == ProductID).FirstOrDefault();
            Cart cart = context.Cart.FirstOrDefault(c => c.ProductID == psc.ID);

            if (cart == null)
            {
                cart = new Cart();
                cart.ProductID = psc.ID;
                cart.Count = 1;

                CartNotifyViewModel cartNotifyViewModel = new CartNotifyViewModel();
                cartNotifyViewModel.Size = context.Size.Where(s => s.ID == size).Select(s => s.size).FirstOrDefault();
                cartNotifyViewModel.Color = context.Color.Where(s => s.ID == color).Select(c => c.Name).FirstOrDefault();
                var user = context.Users.FirstOrDefault(u => u.Id == UserID);
                if (user == null)
                {
                    Clients.All.SendAsync("NotSignIn");
                }
                else
                {
                    cart.CustomerID =
                        context.Users.Where(u => u.Id == UserID).Select(u => u.Id).FirstOrDefault();

                    context.Cart.Add(cart);
                    context.SaveChanges();
                    cartNotifyViewModel.Cart = cart;

                    int c = context.Cart.ToList().Count;



                    Clients.All.SendAsync("CardAdded", cartNotifyViewModel, c);
                }

            }
            else
            {
                Clients.All.SendAsync("CardAdded", null, 0);
            }

        }
    }
}
