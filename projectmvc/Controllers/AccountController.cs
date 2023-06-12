
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using projectmvc.Models;
using projectmvc.ViewModel;

namespace projectmvc.Controllers
{
    public class AccountController : Controller
    {
        Context Context = new Context();
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController
            (UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = _userManager;
            this.signInManager = signInManager;
        }



        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]

        public IActionResult ShipperRegistration()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShipperRegistration (RegistrationViewModel UserviewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = new ApplicationUser();
                userModel.UserName = UserviewModel.UserName;
                userModel.PasswordHash = UserviewModel.Password;
                userModel.Address = UserviewModel.Address;

                IdentityResult result =
                    await userManager.CreateAsync(userModel, UserviewModel.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(userModel, "Shipper");
                    Shipper NewShipper = new Shipper();
                    NewShipper.Id = userModel.Id;
                    NewShipper.IsValid = true;
                    Context.Shippers.Add(NewShipper);
                    Context.SaveChanges();
                    
                    await signInManager.SignInAsync(userModel, false); 
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }

            return View(UserviewModel);
        }


        [HttpGet]
        public IActionResult CustomerRegistration()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CustomerRegistration
            (RegistrationViewModel UserviewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = new ApplicationUser();
                userModel.UserName = UserviewModel.UserName;
                userModel.PasswordHash = UserviewModel.Password;
                userModel.Address = UserviewModel.Address;

                IdentityResult result =
                    await userManager.CreateAsync(userModel, UserviewModel.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(userModel, "Customer");

                    await signInManager.SignInAsync(userModel, false);
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }

            return View(UserviewModel);
        }


        [HttpGet]
        public IActionResult SupplierRegistration()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SupplierRegistration
            (RegistrationViewModel UserviewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = new ApplicationUser();
                userModel.UserName = UserviewModel.UserName;
                userModel.PasswordHash = UserviewModel.Password;
                userModel.Address = UserviewModel.Address;

                IdentityResult result =
                    await userManager.CreateAsync(userModel, UserviewModel.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(userModel, "Supplier");

                    await signInManager.SignInAsync(userModel, false);
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }


            }

            return View(UserviewModel);
        }



        [HttpGet]

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult>  Login(LoginViewModel UserFromReq)
        {

            if (ModelState.IsValid)
            {
                ApplicationUser userModel = await userManager.FindByNameAsync(UserFromReq.Username);
                if (userModel != null)
                
                {

                    Microsoft.AspNetCore.Identity.SignInResult result =
                        await signInManager.PasswordSignInAsync(userModel, UserFromReq.Password, UserFromReq.rememberMe, false);
                  


                    if (result.Succeeded && await userManager.IsInRoleAsync(userModel, "Supplier"))
                    {

                        return RedirectToAction("Index", "Supplier");

                    }
                    else if (result.Succeeded && await userManager.IsInRoleAsync(userModel, "Customer"))
                    {
                        return RedirectToAction("Index", "Category");


                    }
                    else if (result.Succeeded && await userManager.IsInRoleAsync(userModel, "admin"))
                    {
                        return RedirectToAction("Order", "Admin");
                    }
                    else if (result.Succeeded && await userManager.IsInRoleAsync(userModel, "Shipper"))
                    {
                        return RedirectToAction("index", "Shipper");
                    }
                    else
                    {
                        return Content("You Don't Have Access");

                    }

                }
                else
                {
                    ModelState.AddModelError("", " Wrong Data Try Again !!");
                }

            }

            return View(UserFromReq);
        }

        /*  [HttpGet]
          public IActionResult CustomerLogin()
          {
              return View();
          }
          [HttpPost]
          public async Task<IActionResult> CustomerLogin(LoginViewModel UserFromReq)//username,password,remeberme 
          {
              if (ModelState.IsValid)
              {
                  //check valid  account "found in db"

                  ApplicationUser userModel =
                      await userManager.FindByNameAsync(UserFromReq.Username);
                  if (userModel != null)
                  {
                      //cookie
                      Microsoft.AspNetCore.Identity.SignInResult rr =
                          await signInManager.PasswordSignInAsync(userModel, UserFromReq.Password, UserFromReq.rememberMe, false);
                      //check cookie
                      if (rr.Succeeded)

                      return RedirectToAction("Index", "Category");
                     //we need to add else 
                  }
                  else
                  {
                      ModelState.AddModelError("", " Wrong Data Try Again !!");
                  }
              }
              return View(UserFromReq);
          }

          [HttpGet]

          public IActionResult ShipperLogin()
          {
              return View();
          }
          [HttpPost]
          public async Task<IActionResult> ShipperLogin(LoginViewModel UserFromReq)//username,password,remeberme 
          {
              if (ModelState.IsValid)
              {
                  //check valid  account "found in db"

                  ApplicationUser userModel =
                      await userManager.FindByNameAsync(UserFromReq.Username);
                  if (userModel != null)
                  {
                      //cookie
                      Microsoft.AspNetCore.Identity.SignInResult rr =
                          await signInManager.PasswordSignInAsync(userModel, UserFromReq.Password, UserFromReq.rememberMe, false);
                      //check cookie
                      if (rr.Succeeded)
                          return RedirectToAction("Index");
                      //we need to add else 
                  }
                  else
                  {
                      ModelState.AddModelError("", " Wrong Data Try Again !!");
                  }
              }
              return View(UserFromReq);
          }

          [HttpGet]
          public IActionResult SupplierLogin()
          {
              return View();
          }

          [HttpPost]
          public async Task<IActionResult> SupplierLogin(LoginViewModel UserFromReq)//username,password,remeberme 
          {
              if (ModelState.IsValid)
              {
                  //check valid  account "found in db"

                  ApplicationUser userModel =
                      await userManager.FindByNameAsync(UserFromReq.Username);
                  if (userModel != null)
                  {
                      //cookie
                      Microsoft.AspNetCore.Identity.SignInResult rr =
                          await signInManager.PasswordSignInAsync(userModel, UserFromReq.Password, UserFromReq.rememberMe, false);
                      //check cookie
                      if (rr.Succeeded)
                          return RedirectToAction("Index","Supplier");
                      //we need to add else 
                  }
                  else
                  {
                      ModelState.AddModelError("", " Wrong Data Try Again !!");
                  }
              }
              return View(UserFromReq);
          }*/


        public async Task<IActionResult> signOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index","Category");
        }

    }
}
