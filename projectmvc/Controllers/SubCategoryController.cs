using Microsoft.AspNetCore.Mvc;
using projectmvc.Models;
using projectmvc.Repository;

namespace projectmvc.Controllers
{
    public class SubCategoryController : Controller
    {
        Context Context = new Context();
        IProductsRepository productsRepository;
        IProductSizeColorRepository ProductSizeColorRepository;
        ICartRepository cartRepository;
        ISubCategoryRepository subCategoryRepository;
        ICategoryRepository categoryRepository;

        public SubCategoryController(IProductsRepository _productsRepository,
            IProductSizeColorRepository _productSizeColorRepository, 
            ICartRepository _cartRepository, 
            ICategoryRepository categoryRepository,
            ISubCategoryRepository subCategoryRepository
                )
        {
            productsRepository = _productsRepository;
            ProductSizeColorRepository = _productSizeColorRepository;
            cartRepository = _cartRepository;
            this.categoryRepository = categoryRepository;
            this.subCategoryRepository = subCategoryRepository;
        }


        // /SubCategory/Index
        public IActionResult Index()
        {
            return View();
        }


        // / SubCategory/SingleProduct
        public IActionResult SingleProduct(int id)
        {
            return Json(subCategoryRepository.GetSubCatById(id));
        }


        // /SubCategory/AllProduct
        public IActionResult AllProduct()
        {
            return View(subCategoryRepository.GetAll());
        }

    }
}
