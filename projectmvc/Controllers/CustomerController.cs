using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectmvc.Models;

namespace projectmvc.Controllers
{
    public class CustomerController : Controller
    {
        Context Context = new Context();
        public IActionResult ShowOrder()
        {
            string CustomerID = Context.Users.Where(u => u.UserName == User.Identity.Name).Select(u => u.Id).FirstOrDefault();   
            return View(Context.Orders.Include(o=>o.OrderStatus).Include(c=>c.Shipper).ThenInclude(s=>s.shipper).Where(o=>o.CustomerID==CustomerID).ToList());
        }
    }
}
