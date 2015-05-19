using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SQLClient_Products.Models
{
    public class ProductImageRepository
    {
        // InsertProductImage - inserts a product into the database
        public static bool InsertProductImage(string imageUrl, int productId)
        {
            using (
                SqlConnection con =
                    new SqlConnection(
                        System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"]
                            .ConnectionString))
            {
                //open connection
                con.Open();
                //try con
                try
                {
                    //make command
                    using (
                        SqlCommand command =
                            new SqlCommand(
                                "INSERT INTO ProductImages (ImageURL,ProductId,Price) VALUES(@imageUrl, @productId)",
                                con))
                    {
                        // make parameters
                        command.Parameters.Add(new SqlParameter("imageUrl", imageUrl));
                        command.Parameters.Add(new SqlParameter("productId", productId));
                        //execute
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }
        // DeleteProductImage - deletes a product in the database
        public static bool DeleteProductImage(int id)
        {
            //make connection
            using (
                SqlConnection con =
                    new SqlConnection(
                        System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"]
                            .ConnectionString))
            {
                //open the connection
                con.Open();
                //try to access
                try
                {
                    //make command
                    using (
                        SqlCommand command = new SqlCommand("DELETE FROM ProductImages WHERE ProductImageId = @id", con)
                        )
                    {
                        //make parameter
                        command.Parameters.Add(new SqlParameter("id", id));
                        //execute
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }
        public static List<ProductImage> GetAllProductImages()
        {
            //make connection
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //open connection
                con.Open();
                //try connection
                try
                {
                    //make command
                    using (SqlCommand command = new SqlCommand("SELECT * FROM ProductImages WHERE productId = @id", con))
                    {
                        //add all products to new list
                        List<ProductImage> allProductsImage = new List<ProductImage>();
                        //make reader
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int pIid = reader.GetInt32(0);
                            int pid = reader.GetInt32(1);
                            string imageurl = reader.GetString(2);
                            allProductsImage.Add(new ProductImage(pIid, pid, imageurl));
                        }
                        return allProductsImage;
                    }
                }
                catch
                {
                    return new List<ProductImage>();
                }
            }
        }
    }
}