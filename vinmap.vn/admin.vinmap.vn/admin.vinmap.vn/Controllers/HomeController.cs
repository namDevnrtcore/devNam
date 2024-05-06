using admin.vinmap.vn.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace admin.vinmap.vn.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RepositoryProduct _repositoryProduct;
        public HomeController(ILogger<HomeController> logger, IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            _repositoryProduct = new RepositoryProduct(hostingEnvironment);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string user, string pass)
        {
           
            if (user == "vinmap.vn" && pass == "vinmap12345")
            {
                // Tạo claims cho người dùng
                var claims = new[]
                {
                new Claim(ClaimTypes.Name, "Admin"),
                new Claim(ClaimTypes.Role, "User"), 
            };

                // Tạo ClaimsIdentity
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Tạo Principal
                var principal = new ClaimsPrincipal(claimsIdentity);

                // Tạo token và đặt thời gian tồn tại 7 ngày
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.Add(TimeSpan.FromDays(7))
                };

                // Đăng nhập người dùng
                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    authProperties);

                // Chuyển hướng tới trang Home/Index
                return RedirectToAction("Index", "Home");
            }
            else
            {                
                return RedirectToAction("Login", "Home"); 
            }
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult TinTuc()
        {

            List<TinTuc> ListTinTuc = _repositoryProduct.ListTinTuc();
            return View(ListTinTuc);


        }
        public IActionResult Product()
        {
            
                List<ListProduct> ListProduct = _repositoryProduct.ListProduct();
                return View(ListProduct);                
        }
        public IActionResult AddProduct()
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(ProductModel model)
        {        
           if(model.ProductName == null)
            {
                TempData["ErrorMessage"] = "Tên sản phẩm không được để trống.";
                return View();
            }
            if (model.ProductPrice == null|| model.ProductPrice==0)
            {
                TempData["ErrorMessage"] = "Giá sản phẩm không được để trống.";
                return View();
            }          
            if (model.ProductNote == null)
            {
                TempData["ErrorMessage"] = "Ghi chú sản phẩm không được để trống.";
                return View();
            }
            if (model.ProductSpec == null)
            {
                TempData["ErrorMessage"] = "Thông số sản phẩm không được để trống.";
                return View();
            }
            if (model.ProductMainImage == null)
            {
                TempData["ErrorMessage"] = "Ảnh chính sản phẩm không được để trống.";
                return View();
            }
            
            bool Addnew = _repositoryProduct.AddNewProduct(model);
            if(Addnew == true)
            {
                TempData["ErrorMessage"] = "Thêm mới thành công";
                return RedirectToAction("Product");
            }
            else
            {
                TempData["ErrorMessage"] = "Thêm mới thất bại vui lòng thử lại";
                return View();
            }
        }
        
        [HttpPost]
        public JsonResult DeleteProduct(string id)
        {
            bool result = _repositoryProduct.DeleteProduct(id);
            return Json(result);
        }
        public IActionResult UpdateProduct(int id)
        {
            ListProduct product = _repositoryProduct.UpdateProduct(id);
            return View(product);
        }
        public IActionResult EditIMG(int id)
        {
            List<ProductImage> product = _repositoryProduct.UpdateIMG(id);
            return View(product);
        }
        [HttpPost]
        public ActionResult UpdateIMG( IFormFile ProductDetailImages, int NewID)
        {
            bool IMG = _repositoryProduct.EditIMG(ProductDetailImages, NewID);
            return Json(IMG);
        }

        [HttpPost]
        public ActionResult DeleteImage( int imageId)
        {
            bool IMG = _repositoryProduct.DeleteImage(imageId);
            return Json(IMG);
        }
        [HttpPost]
        public ActionResult AddNewImage(IFormFile newImage, int IDIMG)
        {                        
                bool result = _repositoryProduct.AddNewImage(newImage, IDIMG);            
                return Json(result);           
        }



        [HttpPost]
        public IActionResult UpdateProduct(ProductModel productModel)
        {


            if (productModel.ProductName == null)
            {
                TempData["ErrorMessage"] = "Tên sản phẩm không được để trống.";
                return View();
            }
            if (productModel.ProductPrice == null || productModel.ProductPrice == 0)
            {
                TempData["ErrorMessage"] = "Giá sản phẩm không được để trống.";
                return View();
            }
            if (productModel.ProductNote == null)
            {
                TempData["ErrorMessage"] = "Ghi chú sản phẩm không được để trống.";
                return View();
            }
            if (productModel.ProductSpec == null)
            {
                TempData["ErrorMessage"] = "Thông số sản phẩm không được để trống.";
                return View();
            }
           

            bool Addnew = _repositoryProduct.UpdateProduct(productModel);
            if (Addnew == true)
            {
                TempData["ErrorMessage"] = "Update thành công";
                return RedirectToAction("Product");
            }
            else
            {
                TempData["ErrorMessage"] = "Update thất bại vui lòng thử lại";
                return View();
            }

        }


        public IActionResult AddTinTuc()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult AddTinTuc(AddTinTuc model)
        {
            bool tintuc = _repositoryProduct.AddTinTuc(model);
            if (tintuc == true)
            {
                TempData["ErrorMessage"] = "Thêm mới thành công";
                return RedirectToAction("TinTuc");
            }
            else
            {
                TempData["ErrorMessage"] = "Thêm mới thất bại";
                return RedirectToAction("TinTuc");
            }
        }
        public IActionResult UpdateTinTuc(int id)
        {
            TinTuc product = _repositoryProduct.UpdateTinTuc(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult UpdateTinTuc(TinTuc model)
        {
           bool tintuc = _repositoryProduct.SaveTinTuc(model);
            if(tintuc == true)
            {
                TempData["ErrorMessage"] = "Sửa đổi thành công";
                return RedirectToAction("TinTuc");
            }
            else
            {
                TempData["ErrorMessage"] = "Sửa đổi thất bại";
                return RedirectToAction("TinTuc");
            }
        }


        [HttpPost]
        public ActionResult DeleteTintuc(int id)
        {
            bool IMG = _repositoryProduct.DeleteTintuc(id);
            return Json(IMG);
        }
        public IActionResult Banner()
        {           
            Banner banners = _repositoryProduct.GetBanners();          
            return View(banners);
        }
        [HttpPost]
        public IActionResult Banner(Banner1 banner)
        {

            bool banners = _repositoryProduct.UpdateBanners(banner);

            if (banners == true)
            {
                TempData["ErrorMessage"] = "Sửa đổi thành công";
                return RedirectToAction("Banner");
            }
            else
            {
                TempData["ErrorMessage"] = "Sửa đổi thất bại";
                return RedirectToAction("Banner");
            }
        }
        public IActionResult DonHang()
        {
            List<DonHang> donHangs = _repositoryProduct.DonHang();
            return View(donHangs);
        }
    }
}
