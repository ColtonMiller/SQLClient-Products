using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace SQLClient_Products.Models
{
    public class ProductRepository
    {
        //TODO: Fill in product data access methods....
        
        // InsertProduct - inserts a product into the database
        public bool InsertProduct(string name, string description, decimal price, string imageUrl)
        {
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //open connection
                con.Open();
                //try con
                try
                {
                    //make command
                    using (SqlCommand command = new SqlCommand("INSERT INTO Products Values (@name, @description,@price, @imageUrl)",con))
                    {
                        // make parameters
                        command.Parameters.Add(new SqlParameter("name", name));
                        command.Parameters.Add(new SqlParameter("description", description));
                        command.Parameters.Add(new SqlParameter("price", price));
                        command.Parameters.Add(new SqlParameter("imageUrl",imageUrl));
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
        // DeleteProduct - deletes a product in the database
        public static bool DeleteProduct(int id)
        {
            //make connection
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //open the connection
                con.Open();
                //try to access
                try
                {
                    //make command
                    using (SqlCommand command = new SqlCommand("DELETE FROM Products WHERE ProductId = @id", con))
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
        // UpdateProduct - updates a product in the database
        public static bool UpdateProduct(int id, string name, string description, double price, string imageUrl)
        {
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //open
                con.Open();
                try
                {
                    //sql call
                    using (SqlCommand command = new SqlCommand("UPDATE Contacts SET Name = @name, Description = @description, Price = @price, ImageURL = @imageUrl WHERE ContactId = @id ", con))
                    {
                        //parameters to avoid Injection
                        command.Parameters.Add(new SqlParameter("id", id));
                        command.Parameters.Add(new SqlParameter("name", name));
                        command.Parameters.Add(new SqlParameter("description", description));
                        command.Parameters.Add(new SqlParameter("price", price));
                        command.Parameters.Add(new SqlParameter("imageUrl", imageUrl));
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
        // GetProductById - gets a single product from the database by it's Id
        public static Product GetProductById(int id)
        {
            //add all contacts to new list
            List<Product> allProducts = new List<Product>();
            //make connection 
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //open connection
                con.Open();
                //try connection
                try
                {
                    //make command
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Contacts", con))
                    {
                        //make reader
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int pId = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string description = reader.GetString(2);
                            decimal price = reader.GetDecimal(3);
                            string imageUrl = reader.GetString(4);
                            allProducts.Add(new Product(pId, name, description, price, imageUrl));
                        }
                        return allProducts.Where(x => x.Id == id).First();
                    }
                }
                catch
                {
                    return new Product();
                }
            }
        }
        // GetAllProducts - returns all products from the database
        public static List<Product> GetAllProducts()
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
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Products", con))
                    {
                        //add all contacts to new list
                        List<Product> allProducts = new List<Product>();
                        //make reader
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int pid = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string description = reader.GetString(2);
                            decimal price = reader.GetDecimal(3);
                            string imageUrl = reader.GetString(4);
                            allProducts.Add(new Product(pid, name, description, price, imageUrl));
                        }
                        return allProducts;
                    }
                }
                catch
                {
                    return new List<Product>();
                }
            }
        }
    }
}