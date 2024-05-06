using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace admin.vinmap.vn.Models
{
    public class ProductModel
    {
        public string ID { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public string ProductType { get; set; }
        public string ProductNote { get; set; }
        public string ProductSpec { get; set; }
        public IFormFile ProductMainImage { get; set; }
        public List<IFormFile> ProductDetailImages { get; set; }

      
       

    }
    public class ProductImage
    {
        public int IDIMG { get; set; }
        public int NewID { get; set; }
        public string Link { get; set; }
    }

    public class ListProduct
    {
        public int ID { get; set; }
        public string TenSP { get; set; }
        public int GiaSP { get; set; }
        public string LoaiSP { get; set; }
        public string GhiChu { get; set; }
        public string ThongSoSP { get; set; }
        public string IMG { get; set; }
        public List<string> ProductDetailImages { get; set; }
    }
    public class TinTuc
    {
        public int ID { get; set; }
        public string TieuDe { get; set; }
        public string NoiDungTT { get; set; }
        public string NoiDung { get; set; }
        public string IMG { get; set; }
        public IFormFile ProductMainImage { get; set; }

    }
    public class AddTinTuc
    {
    
        public string TieuDe { get; set; }
        public string NoiDungTT { get; set; }
        public string NoiDung { get; set; }
        public string IMG { get; set; }
        public IFormFile ProductMainImage { get; set; }

    }
    public class Banner
    {     
        public string Banner1 { get; set; }
        public string Banner2 { get; set; }
    }
    public class Banner1
    {
        public IFormFile fileBanner1 { get; set; }
        public IFormFile fileBanner2 { get; set; }
    }
    public class DonHang
    {
        public int ID { get; set; }
        public string TenSP { get; set; }
        public string SoLuong { get; set; }
        public int GiaSP { get; set; }
        public int TongTien { get; set; }
        public string HoTen { get; set; }
        public string SDT { get; set; }

        public string IMG { get; set; }
      
    }
}
