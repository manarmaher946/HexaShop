using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using projectmvc.Hubs;
using projectmvc.Models;
using projectmvc.Repository;
using projectmvc.ViewModel;
using System.Data;

namespace projectmvc.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CartController : Controller
    {
        Context Context = new Context();
        ICartRepository CartRepository { get; set; }
        ICategoryRepository CategoryRepository;
        ISubCategoryRepository SubCategoryRepository;
        ISizeRepository SizeRepository;
        IColorRepository ColorRepository;
        IProductsRepository ProductRepository;
        IProductSizeColorRepository ProductSizeColorRepository;

        IHubContext<AddCommentHub> hubContext;
        public CartController(ICategoryRepository category, ISubCategoryRepository subCategory ,ICartRepository cartRepository
            , ISizeRepository sizeRepository, IColorRepository colorRepository, IProductsRepository productsRepository
            , IProductSizeColorRepository productSizeColorRepository,
            IHubContext<AddCommentHub> hubContext
           )
        {
            CategoryRepository = category;
            SubCategoryRepository = subCategory;
            SizeRepository = sizeRepository;
            CartRepository = cartRepository;
            ColorRepository = colorRepository;
            this.ProductRepository = productsRepository;
            ProductSizeColorRepository = productSizeColorRepository;
                 this.hubContext = hubContext;

        }
        public IActionResult Index()
        {
            return View();
        }

 
        public IActionResult AllCustomerProduct()
        {
            ViewBag.DateNow = DateTime.Now.AddDays(4).ToString("dd-MM-yyyy");
            ViewBag.DateArrive = DateTime.Now.AddDays(8).ToString("dd-MM-yyyy");
            ViewBag.UserName = Context.Users.Where(p => p.UserName == User.Identity.Name).FirstOrDefault();
            string id = Context.Users.Where(p => p.UserName == User.Identity.Name).Select(p=>p.Id).FirstOrDefault();
            return View(CartRepository.GetAll(id));
        }
        public IActionResult Delete(int id)
        {
            
            CartRepository.Delete(id);
            return RedirectToAction("AllCustomerProduct");
        }
        public IActionResult UpdateCart(int id, int quantity)
        {

            Context.Cart.FirstOrDefault(c => c.Id == id).Count = quantity;
            Context.SaveChanges();
            return RedirectToAction("AllCustomerProduct", "Cart");


        }

        public IActionResult CheckOut()
        {
            ViewBag.UserName = Context.Users.Where(p => p.UserName == User.Identity.Name).FirstOrDefault();
            string UserId = Context.Users.Where(p => p.UserName == User.Identity.Name).Select(p => p.Id).FirstOrDefault();
            string CustomerAddress = Context.Users.Where(u => u.Id == UserId).Select(s => s.Address).FirstOrDefault();
            var productSizeColors = Context.Cart.Where(c=>c.CustomerID== UserId).Include(p => p.ProductSizeColor).ThenInclude(p=>p.Product).Select(p=> new { p.ProductSizeColor ,p.Count}).ToList();
            int StatusID = Context.OrderStatuses.Where(e => e.Name == "Pending").Select(e => e.Id).First();
            Orders order = new Orders();
            order.CustomerID = UserId;
            order.OrderDate = DateTime.Now;
            order.OrderStatusID  = StatusID;
            order.Address = CustomerAddress;
            order.Discount = 0;
            order.TotalPrice = productSizeColors.Sum(p => p.ProductSizeColor.Product.PriceafterProfit * p.Count);
            Context.Orders.Add(order);
            Context.SaveChanges();

            foreach (var item in productSizeColors)
            {
                OrderItems orderItems = new OrderItems();
                orderItems.OrderID = order.Id;
                orderItems.Quentity = Context.Cart.FirstOrDefault(c=>c.ProductID == item.ProductSizeColor.ID &&c.CustomerID == UserId).Count;
                orderItems.ProductID = item.ProductSizeColor.ID;
                Context.ProductSizeColor.FirstOrDefault(p => p.ID == item.ProductSizeColor.ID).Quantity -= orderItems.Quentity;
                Context.OrderItems.Add(orderItems);
                Context.SaveChanges();
            }


            List<Cart> carts = Context.Cart.Where(c => c.CustomerID == UserId).ToList();
            foreach(Cart c in carts)
            {
                Context.Cart.Remove(c);
            }
            Context.SaveChanges();


            return RedirectToAction("Index", "Category");

        }
    }
}
