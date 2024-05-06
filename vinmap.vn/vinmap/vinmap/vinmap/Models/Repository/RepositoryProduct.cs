
using Dapper;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using vinmap.Models;

public class RepositoryProduct

{
    private readonly string _connectionString = "Data Source=112.78.2.156;Initial Catalog=vin06887_vimap.vn;User ID=vin06887_vin06887;Password=MiKa@6KaGe$2LaGu";
    private readonly IHostingEnvironment _hostingEnvironment;

    public RepositoryProduct(IHostingEnvironment hostingEnvironment)
    {
        _hostingEnvironment = hostingEnvironment;
    }




    public List<ListProduct> ListProduct()
    {
        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        {
            dbConnection.Open();

            // Lấy danh sách tin tức từ bảng TinTuc
            string tinTucQuery = "SELECT * FROM TinTuc";
            var commonTinTucs = dbConnection.Query<TinTuc>(tinTucQuery).ToList();

            string BannerQuery = "SELECT * FROM Banner where ID = 1";
            var commonBanner = dbConnection.Query<Banner>(BannerQuery).ToList();

            // Lấy toàn bộ sản phẩm từ bảng Product
            string productQuery = "SELECT * FROM Product";
            var products = dbConnection.Query<ListProduct>(productQuery).ToList();

            // Lặp qua từng sản phẩm để lấy danh sách hình ảnh chi tiết
            foreach (var product in products)
            {
                // Lấy danh sách hình ảnh chi tiết từ bảng ProductImage dựa trên ID của sản phẩm
                string imageQuery = "SELECT Link FROM ProductImage WHERE IDIMG = @ProductId";
                var images = dbConnection.Query<string>(imageQuery, new { ProductId = product.ID }).ToList();

              
                product.ProductDetailImages = images;
                product.TinTucs = commonTinTucs;
                product.Banners = commonBanner;
            }

            dbConnection.Close();

            return products;
        }
    }

    public List<ListProduct> SearchProduct(string searchTerm)
    {
        if (searchTerm == "camerahanhtrinh")
        {
            searchTerm = "Camera hành trình";
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();


                string productQuery = "SELECT * FROM Product WHERE LoaiSP = @SearchTerm";
                var products = dbConnection.Query<ListProduct>(productQuery, new { SearchTerm = searchTerm }).ToList();


                foreach (var product in products)
                {

                    string imageQuery = "SELECT Link FROM ProductImage WHERE IDIMG = @ProductId";
                    var images = dbConnection.Query<string>(imageQuery, new { ProductId = product.ID }).ToList();

                    // Gán danh sách hình ảnh chi tiết vào sản phẩm
                    product.ProductDetailImages = images;
                }

                dbConnection.Close();

                return products;
            }
        }
        if (searchTerm == "cameranghidinh")
        {

            searchTerm = "Camera nghị định 10";
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();


                string productQuery = "SELECT * FROM Product WHERE LoaiSP = @SearchTerm";
                var products = dbConnection.Query<ListProduct>(productQuery, new { SearchTerm = searchTerm }).ToList();


                foreach (var product in products)
                {

                    string imageQuery = "SELECT Link FROM ProductImage WHERE IDIMG = @ProductId";
                    var images = dbConnection.Query<string>(imageQuery, new { ProductId = product.ID }).ToList();

                    // Gán danh sách hình ảnh chi tiết vào sản phẩm
                    product.ProductDetailImages = images;
                }

                dbConnection.Close();

                return products;
            }
        }
        if (searchTerm == "cameraxekhach")
        {
            searchTerm = "Camera xe khách";
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();


                string productQuery = "SELECT * FROM Product WHERE LoaiSP = @SearchTerm";
                var products = dbConnection.Query<ListProduct>(productQuery, new { SearchTerm = searchTerm }).ToList();


                foreach (var product in products)
                {

                    string imageQuery = "SELECT Link FROM ProductImage WHERE IDIMG = @ProductId";
                    var images = dbConnection.Query<string>(imageQuery, new { ProductId = product.ID }).ToList();

                    // Gán danh sách hình ảnh chi tiết vào sản phẩm
                    product.ProductDetailImages = images;
                }

                dbConnection.Close();

                return products;
            }
        }
        if (searchTerm == "1trieuden5trieu")
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();

                string productQuery = "SELECT * FROM Product WHERE GiaSP >= 1000000 AND GiaSP <= 5000000";
                var products = dbConnection.Query<ListProduct>(productQuery, new {}).ToList();

                foreach (var product in products)
                {
                    string imageQuery = "SELECT Link FROM ProductImage WHERE IDIMG = @ProductId";
                    var images = dbConnection.Query<string>(imageQuery, new { ProductId = product.ID }).ToList();

                   
                    product.ProductDetailImages = images;
                }

                dbConnection.Close();

                return products;
            }
        }
        if (searchTerm == "5trieuden10trieu")
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();

                string productQuery = "SELECT * FROM Product WHERE GiaSP >= 5000000 AND GiaSP <= 10000000 ";
                var products = dbConnection.Query<ListProduct>(productQuery, new {}).ToList();

                foreach (var product in products)
                {
                    string imageQuery = "SELECT Link FROM ProductImage WHERE IDIMG = @ProductId";
                    var images = dbConnection.Query<string>(imageQuery, new { ProductId = product.ID }).ToList();

                    // Gán danh sách hình ảnh chi tiết vào sản phẩm
                    product.ProductDetailImages = images;
                }

                dbConnection.Close();

                return products;
            }
        }
        if (searchTerm == "tren10trieu")
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();

                string productQuery = "SELECT * FROM Product WHERE GiaSP > 10000000 ";
                var products = dbConnection.Query<ListProduct>(productQuery, new { }).ToList();

                foreach (var product in products)
                {
                    string imageQuery = "SELECT Link FROM ProductImage WHERE IDIMG = @ProductId";
                    var images = dbConnection.Query<string>(imageQuery, new { ProductId = product.ID }).ToList();

                    // Gán danh sách hình ảnh chi tiết vào sản phẩm
                    product.ProductDetailImages = images;
                }

                dbConnection.Close();

                return products;
            }
        }      
        else
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();


                string productQuery = "SELECT * FROM Product WHERE TenSP LIKE @SearchTerm";
                var products = dbConnection.Query<ListProduct>(productQuery, new { SearchTerm = $"%{searchTerm}%" }).ToList();


                foreach (var product in products)
                {

                    string imageQuery = "SELECT Link FROM ProductImage WHERE IDIMG = @ProductId";
                    var images = dbConnection.Query<string>(imageQuery, new { ProductId = product.ID }).ToList();

                    // Gán danh sách hình ảnh chi tiết vào sản phẩm
                    product.ProductDetailImages = images;
                }

                dbConnection.Close();

                return products;
            }
        }
    }







    public List<ListProduct> ChiTietSanPham(int ID)
    {
        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        {
            dbConnection.Open();

           
            string productQuery = "SELECT * FROM Product WHERE ID = @ProductId";
            var productDetails = dbConnection.Query<ListProduct>(productQuery, new { ProductId = ID }).FirstOrDefault();

           
            if (productDetails != null)
            {
                
                string loaiSP = productDetails.LoaiSP;

                // Lấy toàn bộ sản phẩm từ bảng Product có cùng LoaiSP
                string productsWithSameLoaiSPQuery = "SELECT * FROM Product WHERE LoaiSP = @LoaiSP";
                var products = dbConnection.Query<ListProduct>(productsWithSameLoaiSPQuery, new { LoaiSP = loaiSP }).ToList();

                // Remove the product with the specified ID from its current position
                products.RemoveAll(p => p.ID == ID);

                // Insert the product with the specified ID at the beginning of the list
                products.Insert(0, productDetails);

                // Lặp qua từng sản phẩm để lấy danh sách hình ảnh chi tiết
                foreach (var product in products)
                {
                    // Lấy danh sách hình ảnh chi tiết từ bảng ProductImage dựa trên ID của sản phẩm
                    string imageQuery = "SELECT Link FROM ProductImage WHERE IDIMG = @ProductId";
                    var images = dbConnection.Query<string>(imageQuery, new { ProductId = product.ID }).ToList();

                    // Gán danh sách hình ảnh chi tiết vào sản phẩm
                    product.ProductDetailImages = images;
                }

                dbConnection.Close();

                return products;
            }

            // If the product details were not found, return an empty list
            dbConnection.Close();
            return new List<ListProduct>();
        }
    }



    public List<TinTuc> ChiTietTinTuc(int ID)
    {
        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        {
            dbConnection.Open();

            // Lấy danh sách tin tức từ bảng TinTuc
            string tinTucQuery = "SELECT * FROM TinTuc where ID ='"+ID+"'";
            var commonTinTucs = dbConnection.Query<TinTuc>(tinTucQuery).ToList();

            

            dbConnection.Close();

            return commonTinTucs;
        }
    }



    public List<TinTuc> ListTinTuc()
    {
        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        {
            dbConnection.Open();

            // Lấy danh sách tin tức từ bảng TinTuc
            string tinTucQuery = "SELECT * FROM TinTuc ";
            var commonTinTucs = dbConnection.Query<TinTuc>(tinTucQuery).ToList();



            dbConnection.Close();

            return commonTinTucs;
        }
    }



    public SelectedProduct YeuCauThanhToan(int ID, int soluong)
    {
        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        {
            dbConnection.Open();

            string productQuery = "SELECT TenSP as Name, GiaSP as Price, IMG FROM Product WHERE ID = @ProductId";
            var productDetails = dbConnection.Query<SelectedProduct>(productQuery, new { ProductId = ID }).FirstOrDefault();

          
            if (productDetails != null)
            {
                productDetails.productQuantity = soluong.ToString();
                productDetails.Total = productDetails.Price * soluong;
            }

            dbConnection.Close();
            return productDetails;
        }
    }


    public bool DonHang(string hoTen, string sdt, string giaTien, string soLuong, string tongTien, string hinhAnh, string tenSP)
    {
        try
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();

                // Insert the order details into the DonHang table
                string insertQuery = "INSERT INTO DonHang (TenSP, GiaSP, SoLuong, TongTien, HoTen, SDT, IMG) VALUES (@TenSP, @GiaSP, @SoLuong, @TongTien, @HoTen, @SDT, @IMG)";
                dbConnection.Execute(insertQuery, new { TenSP = tenSP, GiaSP = giaTien, SoLuong = soLuong, TongTien = tongTien, HoTen = hoTen, SDT = sdt, IMG = hinhAnh });

                dbConnection.Close();
            }

            return true;
        }
        catch (Exception ex)
        {
          
            return false;
        }
    }


}
