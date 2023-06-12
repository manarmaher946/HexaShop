using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using projectmvc.Hubs;
using projectmvc.Models;
using projectmvc.Repository;

namespace projectmvc.Controllers
{
    public class ProductController : Controller
    {
        Context Context = new Context();
        IProductsRepository productsRepository;
        IProductSizeColorRepository ProductSizeColorRepository;
        ICartRepository cartRepository;
        IHubContext<AddCommentHub> CommentHub;
        IHubContext<AddProductHub> ProductHub;

        private readonly IWebHostEnvironment _webHost;
        public ProductController
            (IProductsRepository _productsRepository, 
            IProductSizeColorRepository _productSizeColorRepository
            ,ICartRepository _cartRepository,
            IHubContext<AddCommentHub> _CommentHub,
            IHubContext<AddProductHub> _ProductHub
        


            )
        {
            productsRepository = _productsRepository;
            ProductSizeColorRepository = _productSizeColorRepository;
            cartRepository = _cartRepository;
            CommentHub = _CommentHub;
            ProductHub = _ProductHub;
         
            
        }
      
        [Authorize(Roles = "Customer")]
        public IActionResult SingleProduct(int id)
        {
            ViewBag.UserName = Context.Users.Where(p => p.UserName == User.Identity.Name).FirstOrDefault();
            ViewBag.UserId = Context.Users.Where(p => p.UserName == User.Identity.Name).Select(e=>e.Id).FirstOrDefault();
            ViewBag.Sizes = productsRepository.sizes(id);
            ViewBag.Colors = productsRepository.Colors(id);

            //ViewBag.Sizes = productsRepository.sizes(id);
            ViewBag.AllComents=Context.Review.Where(e=>e.ProductID==id).ToList();
            
            return View(ProductSizeColorRepository.GetProductSizeColor(id));
        }

        public IActionResult GetColors(int ProductID,int SizeID)
        {
            var Colors = Context.ProductSizeColor.Where(p => p.ProductID == ProductID && p.ProductSizeID == SizeID && p.Quantity > 0).Select(c => new { c.Color.ID ,c.Color.Name }).ToList();
            return Json(Colors);
        }

        //[Authorize(Roles = "Admin")]
        public IActionResult AllProduct()
        {
            ViewBag.UserName = Context.Users.Where(p => p.UserName == User.Identity.Name).FirstOrDefault();
            ViewBag.Title = Context.Users.Where(p => p.UserName == User.Identity.Name).FirstOrDefault();
            return View(productsRepository.GetAll());
        }

    }
}
