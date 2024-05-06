using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace vinmap.Models
{
    public class ProductModel
    {
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public string ProductType { get; set; }
        public string ProductNote { get; set; }
        public string ProductSpec { get; set; }
        public IFormFile ProductMainImage { get; set; }
        public List<IFormFile> ProductDetailImages { get; set; }

        public string hinhanhchinh { get; set; }
        public string hinhanhphu { get; set; }
        public string hinhanhphu1 { get; set; }
        public string hinhanhphu2 { get; set; }
        public string hinhanhphu3 { get; set; }
        public string hinhanhphu4 { get; set; }

        public string hinhanhphu5 { get; set; }

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
        public List<TinTuc> TinTucs { get; set; }
        public List<Banner> Banners { get; set; }
    }
    public class Banner
    {
        public int ID { get; set; }
        public string Banner1 { get; set; }
        public string Banner2 { get; set; }
       

    }
    public class TinTuc
    {
        public int ID { get; set; }
        public string TieuDe { get; set; }
        public string NoiDungTT { get; set; }
        public string NoiDung { get; set; }
        public string IMG { get; set; }

    }

    public class SelectedProduct
    {

        public string Name { get; set; }
        public decimal Price { get; set; }
        public string IMG { get; set; }
        public string productQuantity { get; set; }
        public decimal Total { get; set; }
    }
}
