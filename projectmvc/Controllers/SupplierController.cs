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
    //[Authorize(Roles = "Supplier")]
    public class SupplierController : Controller

    {
        IHubContext<AddProductHub> hubContext;
        Context Context;// = new Context();
        ICategoryRepository CategoryRepository;
        ISubCategoryRepository SubCategoryRepository;
        ISizeRepository SizeRepository;
        IColorRepository ColorRepository;
        IProductsRepository ProductRepository;
        IProductSizeColorRepository ProductSizeColorRepository;
        IWebHostEnvironment _webHost;
        IHubContext<AddProductHub> hubContext2;
        public SupplierController(ICategoryRepository category, ISubCategoryRepository subCategory
            , ISizeRepository sizeRepository, IColorRepository colorRepository, IProductsRepository productsRepository
            , IProductSizeColorRepository productSizeColorRepository,
            IHubContext<AddProductHub> hubContext
            , IWebHostEnvironment webHost,
            IHubContext<AddProductHub> hubContext2)
        {
            CategoryRepository = category;
            SubCategoryRepository = subCategory;
            SizeRepository = sizeRepository;
            ColorRepository = colorRepository;
            this.ProductRepository = productsRepository;
            ProductSizeColorRepository = productSizeColorRepository;
            this.hubContext = hubContext;
            _webHost = webHost;
            Context = new Context();
            this.hubContext2 = hubContext2;
        }

        
        public IActionResult Index()
        {
            ViewBag.UserName = Context.Users.Where(p => p.UserName == User.Identity.Name).FirstOrDefault();
            ViewBag.Title = Context.Users.Where(p => p.UserName == User.Identity.Name).FirstOrDefault();
            var SupID = Context.Users.Where(p => p.UserName == User.Identity.Name).Select(p => p.Id).FirstOrDefault();

            return View(ProductRepository.GetBySupplierId(SupID));
        }

        [HttpGet]
        public IActionResult AddProduct()
        {


            ViewBag.UserName = Context.Users.Where(p => p.UserName == User.Identity.Name).FirstOrDefault();
            ViewBag.categories = CategoryRepository.GetAll();
            ViewBag.subCategories = SubCategoryRepository.GetAll();
            ViewData["colors"] = ColorRepository.GetAll();
            ViewData["Sizes"] = SizeRepository.GetAll();

            return View();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct([Bind("ProductName,ProductDescription,Price,PriceafterProfit,Rating,Quentity," +
            "CategoryId,SubCategoryId,ColorId,SizeId,ProductImage,ImageFile")] AddProductsViewModel Newmodel)
        {
            if (ModelState.IsValid)
            {
                // save image to wwwRoot/Image
                string wwwRootPath = _webHost.WebRootPath;
                string FileName = Path.GetFileNameWithoutExtension(Newmodel.ImageFile.FileName);
                string Extension = Path.GetExtension(Newmodel.ImageFile.FileName);
                Newmodel.ProductImage = FileName + Extension; /*= Newmodel.ProductImage + DateTime.Now.ToString("yymmss") + Extension;*/
                string path = Path.Combine(wwwRootPath + "/UploadedImages/" + (FileName + Extension));
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await Newmodel.ImageFile.CopyToAsync(fileStream);
                }

                Product newPro = new Product();
                Product ProductExist = await Context.Products.FirstOrDefaultAsync(p => p.Name == Newmodel.ProductName);
                if (ProductExist == null)
                {
                    newPro.Name = Newmodel.ProductName;
                    newPro.Description = Newmodel.ProductDescription;
                    newPro.Price = Newmodel.Price;
                    newPro.PriceafterProfit = Newmodel.PriceafterProfit;
                    newPro.Image = Newmodel.ProductImage;
                    newPro.Rating = Newmodel.Rating;
                    newPro.SubCategoryId = Newmodel.SubCategoryId;
                    newPro.SupplierID = Context.Users.Where(p => p.UserName == User.Identity.Name).Select(d => d.Id).FirstOrDefault();

                    ProductRepository.Insert(newPro);
                    //hubContext.Clients.All.SendAsync("NewProductAdd", newPro);

                    int cid = Context.SubCategory.Where(e => e.Id == newPro.SubCategoryId).Select(e => e.CategoryId).FirstOrDefault();
                    int counter = Context.Products.ToList().Count();

                    hubContext.Clients.All.SendAsync("NewProductAdd", newPro, cid, counter);
                }
                else
                {
                    newPro = ProductExist;
                }
                ProductSizeColor productSizeColor = new ProductSizeColor();

                productSizeColor.ProductID = newPro.Id;
                productSizeColor.ProductColorID = Newmodel.ColorId;
                productSizeColor.ProductSizeID = Newmodel.SizeId;
                productSizeColor.Quantity = Newmodel.Quentity;
                ProductSizeColorRepository.Insert(productSizeColor);

            }

            ViewBag.categories = CategoryRepository.GetAll();
            ViewBag.subCategories = SubCategoryRepository.GetAll();
            ViewBag.colors = ColorRepository.GetAll();
            ViewBag.Sizes = SizeRepository.GetAll();
            return View(Newmodel);


        }
    }
}
