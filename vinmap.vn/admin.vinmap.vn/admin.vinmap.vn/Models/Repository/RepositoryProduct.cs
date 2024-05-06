using admin.vinmap.vn.Models;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

public class RepositoryProduct

{
    private readonly string _connectionString = "Data Source=112.78.2.156;Initial Catalog=vin06887_vimap.vn;User ID=vin06887_vin06887;Password=MiKa@6KaGe$2LaGu";
    private readonly IHostingEnvironment _hostingEnvironment;

    public RepositoryProduct(IHostingEnvironment hostingEnvironment)
    {
        _hostingEnvironment = hostingEnvironment;
    }
    public bool AddNewProduct(ProductModel model)
    {
        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        {
            dbConnection.Open();

            // Lưu ảnh chính
            string productMainImageFileName = Path.GetFileName(model.ProductMainImage.FileName);
            string productMainImagePath = SaveImage(model.ProductMainImage, productMainImageFileName);

            // Lưu thông tin sản phẩm
            string sqlQuery = @"INSERT INTO Product (TenSP, GiaSP, LoaiSP, GhiChu, ThongSoSP, IMG) 
                            VALUES (@ProductName, @ProductPrice, @ProductType, @ProductNote, @ProductSpec, @ProductMainImage);
                            SELECT CAST(SCOPE_IDENTITY() as int);"; // Lấy ID của sản phẩm vừa thêm

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ProductName", model.ProductName);
            parameters.Add("@ProductPrice", model.ProductPrice);
            parameters.Add("@ProductType", model.ProductType);
            parameters.Add("@ProductNote", model.ProductNote);
            parameters.Add("@ProductSpec", model.ProductSpec);
            parameters.Add("@ProductMainImage", productMainImagePath);

            int productId = dbConnection.Query<int>(sqlQuery, parameters).Single();

            // Lưu các hình ảnh chi tiết
            SaveProductDetailImages(dbConnection, productId, model.ProductDetailImages);

            dbConnection.Close();

            return productId > 0;
        }
    }
    public bool UpdateProduct(ProductModel model)
    {
        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        {
            dbConnection.Open();

            
            string updateWithoutImageQuery = @"UPDATE Product 
                                           SET TenSP = @ProductName, 
                                               GiaSP = @ProductPrice, 
                                               LoaiSP = @ProductType, 
                                               GhiChu = @ProductNote, 
                                               ThongSoSP = @ProductSpec
                                           WHERE ID = @ProductID";

            DynamicParameters parametersWithoutImage = new DynamicParameters();
            parametersWithoutImage.Add("@ProductID", model.ID);
            parametersWithoutImage.Add("@ProductName", model.ProductName);
            parametersWithoutImage.Add("@ProductPrice", model.ProductPrice);
            parametersWithoutImage.Add("@ProductType", model.ProductType);
            parametersWithoutImage.Add("@ProductNote", model.ProductNote);
            parametersWithoutImage.Add("@ProductSpec", model.ProductSpec);

            dbConnection.Execute(updateWithoutImageQuery, parametersWithoutImage);

           
            if (model.ProductMainImage != null && model.ProductMainImage.Length > 0)
            {
                string productMainImageFileName = Path.GetFileName(model.ProductMainImage.FileName);
                string productMainImagePath = SaveImage(model.ProductMainImage, productMainImageFileName);

                string updateWithImageQuery = @"UPDATE Product 
                                            SET IMG = @ProductMainImage
                                            WHERE ID = @ProductID";

                DynamicParameters parametersWithImage = new DynamicParameters();
                parametersWithImage.Add("@ProductID", model.ID);
                parametersWithImage.Add("@ProductMainImage", productMainImagePath);

                dbConnection.Execute(updateWithImageQuery, parametersWithImage);
            }

            dbConnection.Close();

            return true;
        }
    }



    private void SaveProductDetailImages(IDbConnection dbConnection, int productId, List<IFormFile> detailImages)
    {
        if (detailImages != null && detailImages.Count > 0)
        {
            foreach (var detailImage in detailImages)
            {
                string detailImageFileName = Path.GetFileName(detailImage.FileName);
                string detailImagePath = SaveImage(detailImage, detailImageFileName);

                // Lưu thông tin hình ảnh vào bảng ProductImage
                string insertImageQuery = "INSERT INTO ProductImage (IDIMG, Link) VALUES (@ProductID, @ImagePath)";
                DynamicParameters imageParameters = new DynamicParameters();
                imageParameters.Add("@ProductID", productId);
                imageParameters.Add("@ImagePath", detailImagePath);

                dbConnection.Execute(insertImageQuery, imageParameters);
            }
        }
    }


    private string SaveImage(IFormFile image, string fileName)
    { 
        string IMGlink = Path.Combine("https://admin.vinmap.vn/assets/img", fileName);
        string imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "assets", "img", fileName);

        using (var fileStream = new FileStream(imagePath, FileMode.Create))
        {
            image.CopyTo(fileStream);
        }

        return IMGlink;
    }
    public List<ListProduct> ListProduct()
    {
        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        {
            dbConnection.Open();

            // Lấy toàn bộ sản phẩm từ bảng Product
            string productQuery = "SELECT * FROM Product";
            var products = dbConnection.Query<ListProduct>(productQuery).ToList();

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
    }
    public List<TinTuc> ListTinTuc()
    {
        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        {
            dbConnection.Open();

            // Lấy toàn bộ sản phẩm từ bảng Product
            string productQuery = "SELECT * FROM TinTuc";
            var products = dbConnection.Query<TinTuc>(productQuery).ToList();

           

            dbConnection.Close();

            return products;
        }
    }

    public bool DeleteProduct(string productId)
    {
        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        {
            dbConnection.Open();

            // Delete related records in ProductImage table
            string deleteProductImagesQuery = "DELETE FROM ProductImage WHERE IDIMG = @ProductId";
            dbConnection.Execute(deleteProductImagesQuery, new { ProductId = productId });

            // Delete product from Product table
            string deleteProductQuery = "DELETE FROM Product WHERE ID = @ProductId";
            int rowsAffected = dbConnection.Execute(deleteProductQuery, new { ProductId = productId });

            dbConnection.Close();

            return rowsAffected > 0;
        }
    }
    public ListProduct UpdateProduct(int id)
    {
        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        {
            dbConnection.Open();

            // Retrieve a single product by ID
            string productQuery = "SELECT * FROM Product WHERE ID = @ProductId";
            var product = dbConnection.QueryFirstOrDefault<ListProduct>(productQuery, new { ProductId = id });

            // If the product is found, retrieve its detailed images
            if (product != null)
            {
                // Retrieve detailed images for the product from the ProductImage table
                string imageQuery = "SELECT Link FROM ProductImage WHERE IDIMG = @ProductId";
                var images = dbConnection.Query<string>(imageQuery, new { ProductId = id }).ToList();

                // Assign the detailed images to the product
                product.ProductDetailImages = images;
            }

            dbConnection.Close();

            return product;
        }
    }

    public List<ProductImage> UpdateIMG(int id)
    {
        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        {
            dbConnection.Open();

            string imageQuery = "SELECT NewID, Link ,IDIMG FROM ProductImage WHERE IDIMG = @ProductId";
            var images = dbConnection.Query<ProductImage>(imageQuery, new { ProductId = id }).ToList();

            dbConnection.Close();

            return images;
        }
    }
    public bool EditIMG(IFormFile ProductDetailImages, int NewID)
    {
        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        {
            dbConnection.Open();


            string productMainImageFileName = Path.GetFileName(ProductDetailImages.FileName);
            string productMainImagePath = SaveImage(ProductDetailImages, productMainImageFileName);

            // Cập nhật database
            string updateQuery = "UPDATE ProductImage SET Link = @Link WHERE NewID = @ProductId";
            int rowsAffected = dbConnection.Execute(updateQuery, new { Link = productMainImagePath, ProductId = NewID });

            dbConnection.Close();

            return rowsAffected > 0;
        }
    }

    public bool DeleteImage( int NewID)
    {
        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        {
            dbConnection.Open();





            string deleteQuery = "DELETE FROM ProductImage WHERE NewID = @ProductId";
            int rowsAffected = dbConnection.Execute(deleteQuery, new { ProductId = NewID });


            dbConnection.Close();

            return rowsAffected > 0;
        }
    }


    public bool DeleteTintuc(int NewID)
    {
        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        {
            dbConnection.Open();

            string deleteQuery = "DELETE FROM TinTuc WHERE ID = @ProductId";
            int rowsAffected = dbConnection.Execute(deleteQuery, new { ProductId = NewID });


            dbConnection.Close();

            return rowsAffected > 0;
        }
    }

    public bool AddNewImage(IFormFile ProductDetailImages, int NewID)
    {
        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        {
            dbConnection.Open();


            string productMainImageFileName = Path.GetFileName(ProductDetailImages.FileName);
            string productMainImagePath = SaveImage(ProductDetailImages, productMainImageFileName);

            // Cập nhật database
            string updateQuery = "INSERT INTO ProductImage (Link, IDIMG) VALUES (@Link, @ProductId)";
            int rowsAffected = dbConnection.Execute(updateQuery, new { Link = productMainImagePath, ProductId = NewID });


            dbConnection.Close();

            return rowsAffected > 0;
        }
    }




    public TinTuc UpdateTinTuc(int id)
    {
        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        {
            dbConnection.Open();

            // Retrieve a single product by ID
            string productQuery = "SELECT * FROM TinTuc WHERE ID = @ProductId";
            var product = dbConnection.QueryFirstOrDefault<TinTuc>(productQuery, new { ProductId = id });

           

            dbConnection.Close();

            return product;
        }
    }


    public bool SaveTinTuc(TinTuc model)
    {
        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        {
            dbConnection.Open();

            // Update the existing record
            string updateQuery = "UPDATE TinTuc SET TieuDe = @TieuDe, NoiDungTT = @NoiDungTT, NoiDung = @NoiDung, IMG = @IMG";

            // If a new image is provided, save it and update the IMG field
            if (model.ProductMainImage != null)
            {
                string productMainImageFileName = Path.GetFileName(model.ProductMainImage.FileName);
                string IMGlink = SaveImage(model.ProductMainImage, productMainImageFileName);
                model.IMG = IMGlink;
            }

            updateQuery += " WHERE ID = @Id";

            dbConnection.Execute(updateQuery, model);

            dbConnection.Close();

            return true;
        }
    }

    public bool AddTinTuc(AddTinTuc model)
    {
        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        {
            dbConnection.Open();

            // Insert new record
            string insertQuery = "INSERT INTO TinTuc (TieuDe, NoiDungTT, NoiDung, IMG) VALUES (@TieuDe, @NoiDungTT, @NoiDung, @IMG)";

            // If a new image is provided, save it and update the IMG field
            if (model.ProductMainImage != null)
            {
                string productMainImageFileName = Path.GetFileName(model.ProductMainImage.FileName);
                string IMGlink = SaveImage(model.ProductMainImage, productMainImageFileName);
                model.IMG = IMGlink;
            }

            dbConnection.Execute(insertQuery, model);

            dbConnection.Close();

            return true;
        }
    }



    public Banner GetBanners()
    {
        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        {
            dbConnection.Open();           
            string bannerQuery = "SELECT TOP 1 * FROM Banner"; 
            var banner = dbConnection.QueryFirstOrDefault<Banner>(bannerQuery);
            dbConnection.Close();
            return banner;
        }
    }
    public bool UpdateBanners(Banner1 banner)
    {
        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        {
            dbConnection.Open();

           
            string bannerQuery = "SELECT TOP 1 * FROM Banner";
            var currentBanner = dbConnection.QueryFirstOrDefault<Banner>(bannerQuery);

           
            if (banner.fileBanner1 != null)
            {
                string productMainImageFileName = Path.GetFileName(banner.fileBanner1.FileName);
                currentBanner.Banner1 = SaveImage(banner.fileBanner1, productMainImageFileName);
            }

            if (banner.fileBanner2 != null)
            {
                string productMainImageFileName = Path.GetFileName(banner.fileBanner2.FileName);
                currentBanner.Banner2 = SaveImage(banner.fileBanner2, productMainImageFileName);
            }

            
            string updateQuery = "UPDATE Banner SET Banner1 = @Banner1, Banner2 = @Banner2 WHERE Id = @Id";
            dbConnection.Execute(updateQuery, new { Banner1 = currentBanner.Banner1, Banner2 = currentBanner.Banner2, Id = 1});

            dbConnection.Close();

            return true;
        }
    }

    public List<DonHang> DonHang()
    {
        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        {
            dbConnection.Open();

           
            string productQuery = "SELECT * FROM DonHang";
            var products = dbConnection.Query<DonHang>(productQuery).ToList();



            dbConnection.Close();

            return products;
        }
    }
}
