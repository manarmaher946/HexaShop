using Microsoft.AspNetCore.Mvc;

namespace projectmvc.Controllers
{
    public class OrderController : Controller
    {
        // in shipper show all order assigned

        // Order/index
        public IActionResult Index()
        {
            return View();
        }

        // Order/Status
        public IActionResult Status()
        {
            return View();
        }

    }
}
