using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using projectmvc.Hubs;
using projectmvc.Models;
using projectmvc.Repository;
using projectmvc.ViewModel;
using System.Data;
using System.Linq;

namespace projectmvc.Controllers
{


    //[Authorize(Roles = "Customer")]
    public class CategoryController : Controller
    {

        IProductsRepository productsRepository;
        IProductSizeColorRepository ProductSizeColorRepository;
        ICartRepository cartRepository;
        ICategoryRepository categoryRepository;
        ISubCategoryRepository subCategoryRepository;
        Context context;
        IHubContext<AddProductHub> AddProductContext;
        public CategoryController
            (IProductsRepository _productsRepository,
            IProductSizeColorRepository _productSizeColorRepository,
            ICartRepository _cartRepository,
            ICategoryRepository _categoryRepository,
            ISubCategoryRepository _subCategoryRepository,
            IHubContext<AddProductHub> _AddProductContext
            )
        {
            productsRepository = _productsRepository;
            ProductSizeColorRepository = _productSizeColorRepository;
            cartRepository = _cartRepository;
            categoryRepository = _categoryRepository;
                        context = new Context ();
            subCategoryRepository = _subCategoryRepository;
            AddProductContext = _AddProductContext;

        }
        public IActionResult Index()
        {

           var cat= categoryRepository.GetAll();
            return View(cat);
        }

       
        public IActionResult GetAllProductByCategory(int id)
        {
            ViewBag.Id = id;
            ViewBag.UserName = context.Users.Where(p => p.UserName == User.Identity.Name).FirstOrDefault();
            List<int> psc = new List<int>();
            List<List<Product>> ps = new List<List<Product>>();
            List<Product> p = new List<Product>();

            List<int> subCategories = subCategoryRepository.ShowListOFSubCategory(id);
            List<Product> Product = new List<Product>();

            foreach (var item in subCategories) {
                Product = ProductSizeColorRepository.GetLists(item);
                ps.Add(Product);
            }

            foreach (var item in ps)
            {
                foreach(var i in item)
                {
                    p.Add(i); 
                }
               
            }

            return View(p);
        }


    }
}
