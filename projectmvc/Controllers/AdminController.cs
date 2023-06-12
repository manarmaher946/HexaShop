using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.Documents;
using Microsoft.CodeAnalysis.Elfie.Model.Tree;
using Microsoft.EntityFrameworkCore;
using projectmvc.Hubs;
using projectmvc.Models;
using projectmvc.Repository;
using System.Data;

namespace projectmvc.Controllers
{

    // [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        Context Context = new Context();
        IProductsRepository productsRepository;
        IProductSizeColorRepository ProductSizeColorRepository;
        ICartRepository cartRepository;
        IHubContext<AddProductHub> ProductHub;
        public AdminController(Context context, 
            IProductsRepository productsRepository, 
            IProductSizeColorRepository productSizeColorRepository, 
            ICartRepository cartRepository,
            IHubContext<AddProductHub> _ProductHub
            )
        {
            Context = context;
            this.productsRepository = productsRepository;
            ProductSizeColorRepository = productSizeColorRepository;
            this.cartRepository = cartRepository;
            ProductHub = _ProductHub;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult showOrdersArrived()
        {
            int OrderID = Context.OrderStatuses.Where(e => e.Name == "Arrived").Select(e => e.Id).First();
            List<Orders> orders = Context.Orders.Include(c=>c.Customer).Include(s=>s.Shipper).ThenInclude(s=>s.shipper).Where(e => e.OrderStatusID == OrderID).ToList();

            return View(orders);
        }
        public IActionResult ShowOrdersShipped()
        {
            int OrderStatusID = Context.OrderStatuses.Where(e => e.Name == "Shipped").Select(e => e.Id).First();
            List<Orders> orders = Context.Orders.Include(o=>o.Shipper).ThenInclude(s=>s.shipper).Include(c=>c.Customer).Where(e => e.OrderStatusID == OrderStatusID).ToList();


            return View(orders);
        }
        public IActionResult ShowShippers()
        {
            string ShipperID = Context.Roles.Where(e => e.NormalizedName == "Shipper").Select(e => e.Id).First();
            List<string> userIDs = Context.UserRoles.Where(e => e.RoleId == ShipperID).Select(e => e.UserId).ToList();
            List<Shipper> Shippers = new List<Shipper>();
            foreach (string userID in userIDs)
            {
                Shippers.Add(Context.Shippers.Include(s=>s.shipper).FirstOrDefault(e => e.Id == userID));
            }

            return View(Shippers);
        }
        public IActionResult showProducts()
        {
            ViewBag.Title = Context.Users.FirstOrDefault(p => p.UserName == User.Identity.Name);
            return View(productsRepository.GetAll());
        }
        public IActionResult showCustomers()
        {
            string CustomerIDs = Context.Roles.Where(e => e.NormalizedName == "CUSTOMER").Select(e => e.Id).First();
            List<string> userIDs = Context.UserRoles.Where(e => e.RoleId == CustomerIDs).Select(e => e.UserId).ToList();
            List<ApplicationUser> customers = new List<ApplicationUser>();
            foreach (string userID in userIDs)
            {
                customers.Add(Context.Users.Where(e => e.Id == userID).First());
            }

            return View(customers);
        }
        public IActionResult showSupplier()
        {
            string SupplierID = Context.Roles.Where(e => e.NormalizedName == "SUPPLIER").Select(e => e.Id).First();
            List<string> userIDs = Context.UserRoles.Where(e => e.RoleId == SupplierID).Select(e => e.UserId).ToList();
            List<ApplicationUser> Suppliers = new List<ApplicationUser>();
            foreach (string userID in userIDs)
            {
                Suppliers.Add(Context.Users.Where(e => e.Id == userID).First());
            }

            return View(Suppliers);
        }
        public IActionResult showProfits()
        {
            List<int> orders = Context.Orders.Select(o => o.Id).ToList();
            List<ProductSizeColor> productSC = new List<ProductSizeColor>();
            List<List<int>> ProductsIDS = new List<List<int>>();
            Dictionary<int,double> CalcProfPerOrder = new Dictionary<int,double>();
            foreach (int item in orders)
            {
                double SumProfit = 0;
                List<OrderItems> PID = new List<OrderItems>();

                PID = Context.OrderItems.Where(e => e.OrderID == item).
                                                   Include(e => e.Product).ThenInclude(e => e.Product).
                                                   ToList();

                foreach (var ID in PID.Select(o => o.ProductID).ToList())
                {
                    ProductSizeColor p = Context.ProductSizeColor.Include(p => p.Product).FirstOrDefault(e => e.ID == ID);
                    SumProfit += (p.Product.PriceafterProfit - p.Product.Price) * PID.Where(p => p.ProductID == ID)
                        .Select(p => p.Quentity)
                        .FirstOrDefault();
                }
                string name = Context.Orders.Where(o => o.Id == item).Select(c => c.Customer.UserName).FirstOrDefault();
                ViewData["Customer"] = name;

                CalcProfPerOrder.Add(item, SumProfit);


            }

            return View(CalcProfPerOrder);
        }
        public IActionResult Order()
        {
            List<string> ShipID = Context.Shippers.Where(s => s.IsValid == true).Select(s => s.Id).ToList();
            List<ApplicationUser> Shipper = new List<ApplicationUser>();
            foreach(string id in ShipID)
            {
                ApplicationUser Sh = new ApplicationUser();
                Sh = Context.Users.FirstOrDefault(s => s.Id == id);
                Shipper.Add(Sh);
            }
            ViewData["Shipper"] = Shipper;
            return View(Context.Orders.Include(o => o.Customer).Where(o=>o.OrderStatusID==3).ToList());
        }
        public IActionResult AssignShipper(int OrderID, string ShipperID)
        {
            Shipper Shipper = Context.Shippers.Include(s=>s.shipper).FirstOrDefault(u => u.Id == ShipperID);
            Orders Order = Context.Orders.FirstOrDefault(o => o.Id == OrderID);
            Order.Shipper = Shipper;
            int OrderStatusID = Context.OrderStatuses.Where(e => e.Name == "Shipped").Select(e => e.Id).FirstOrDefault();
            Order.OrderStatusID = OrderStatusID;
            Order.ShippedDate = DateTime.Now;
            Context.Shippers.FirstOrDefault(s => s.Id == ShipperID).IsValid = false;

            Context.SaveChanges();
            

            return RedirectToAction("Order");
        }




    }
}
