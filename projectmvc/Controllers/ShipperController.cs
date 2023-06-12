using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectmvc.Models;
using projectmvc.ViewModel;
using System.Data;
using System.Security.Claims;

namespace projectmvc.Controllers
{
    [Authorize(Roles = "Shipper")]
    public class ShipperController : Controller
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        // /Shipper/Index
        Context Context = new Context();
        public IActionResult Index()
        {
            string ShiperID = Context.Users.Where(s => s.UserName == User.Identity.Name).Select(s => s.Id).FirstOrDefault();
            int StatusID = Context.OrderStatuses.Where(o => o.Name == "Shipped").Select(o => o.Id).FirstOrDefault();
            List<Orders> Orders = Context.Orders.Include(c=>c.Customer).Where(s => s.ShipperID == ShiperID&&s.OrderStatusID == StatusID).ToList();
            return View(Orders);
        }



        public IActionResult ConfirmDeliver(int OrderID)
        {
            string ShiperID = Context.Users.Where(s => s.UserName == User.Identity.Name).Select(s => s.Id).FirstOrDefault();

            int StatusID = Context.OrderStatuses.Where(o => o.Name == "Arrived").Select(o => o.Id).FirstOrDefault();
            Orders Order = Context.Orders.FirstOrDefault(o => o.Id == OrderID);
            Order.OrderStatusID = StatusID;
            Order.ArrivedDate= DateTime.Now;
            Context.Shippers.FirstOrDefault(s => s.Id == ShiperID).IsValid = true;
            Context.SaveChanges();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> signOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }


       

    }
}
