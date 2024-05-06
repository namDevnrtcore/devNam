using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using vinmap.Models;

namespace vinmap.Controllers
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

        public IActionResult Index()
        {
            if (TempData.ContainsKey("SuccessMessage"))
            {
               
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }
            List<ListProduct> ListProduct = _repositoryProduct.ListProduct();
            return View(ListProduct);
        }
        public IActionResult LienHe()
        {
            return View();
        }
        public IActionResult CameraHanhTrinh()
        {
            List<ListProduct> ListProduct = _repositoryProduct.ListProduct();
            return View(ListProduct);
        }
        public IActionResult CameraNghiDinh()
        {
            List<ListProduct> ListProduct = _repositoryProduct.ListProduct();
            return View(ListProduct);
        }
        public IActionResult ManHinhAndroid()
        {
            List<ListProduct> ListProduct = _repositoryProduct.ListProduct();
            return View(ListProduct);
        }
        public IActionResult DinhViOTo()
        {
            List<ListProduct> ListProduct = _repositoryProduct.ListProduct();
            return View(ListProduct);
        }
        public IActionResult TinTuc()
        {
            List<TinTuc> searchResults = _repositoryProduct.ListTinTuc();
            return View(searchResults);
        }
        public IActionResult Search(string searchTerm)
        {
            if(searchTerm == null)
            {
                List<ListProduct> ListProduct = _repositoryProduct.ListProduct();
                return View(ListProduct);
            }
            else
            {
                List<ListProduct> searchResults = _repositoryProduct.SearchProduct(searchTerm);
                return View(searchResults);
            }
        }
     
      
        public IActionResult ChiTietTinTuc(int ID)
        {
            List<TinTuc> searchResults = _repositoryProduct.ChiTietTinTuc(ID);
            return View(searchResults);
        }
        public IActionResult ChiTietSanPham(int id)
        {

            List<ListProduct> ListProduct = _repositoryProduct.ChiTietSanPham(id);
            return View(ListProduct);
        }
        public IActionResult ThanhToan(int productId, int productQuantity)
        {


            SelectedProduct ListProduct = _repositoryProduct.YeuCauThanhToan(productId, productQuantity);
            return View(ListProduct);
        }
        [HttpPost]
        public IActionResult ThanhToan(string  hoten, string sdt, string giatien , string soluong , string tongtien ,string hinhanh, string tensp)
        {


             bool SaveHoaDon = _repositoryProduct.DonHang(hoten, sdt, giatien,soluong,tongtien,hinhanh,tensp);
            if (SaveHoaDon == true)
            {
                TempData["SuccessMessage"] = "Đơn hàng đã được đặt thành công!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["SuccessMessage"] = "Đặt đơn thất bại vui lòng liên hệ qua Zalo hoặc FaceBook !";
                return View();
            }
           
        }



    }
}
