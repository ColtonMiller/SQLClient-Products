﻿using System;
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
        public static bool InsertProduct(string name, string description, decimal price)
        {
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //open connection
                con.Open();
                //try con
                try
                {
                    //make command
                    using (SqlCommand command = new SqlCommand("INSERT INTO Products (Name,[Description],Price,ImageURL) VALUES(@name, @description,@price)",con))
                    {
                        // make parameters
                        command.Parameters.Add(new SqlParameter("name", name));
                        command.Parameters.Add(new SqlParameter("description", description));
                        command.Parameters.Add(new SqlParameter("price", price));
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
        public static bool UpdateProduct(int id, string name, string description, decimal price)
        {
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //open
                con.Open();
                try
                {
                    //sql call
                    using (SqlCommand command = new SqlCommand("UPDATE Products SET Name = @name, [Description] = @description, Price = @price WHERE ProductId = @id", con))
                    {
                        //parameters to avoid Injection
                        command.Parameters.Add(new SqlParameter("Id", id));
                        command.Parameters.Add(new SqlParameter("Name", name));
                        command.Parameters.Add(new SqlParameter("Description", description));
                        command.Parameters.Add(new SqlParameter("Price", price));
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
            //add all products to new list
            var product = new Product();
            //make connection 
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //open connection
                con.Open();
                //try connection
                try
                {
                    //make command
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Products WHERE ProductId = @id", con))
                    {
                        command.Parameters.Add(new SqlParameter("id", id));
                        //make reader
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int pId = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string description = reader.GetString(2);
                            decimal price = reader.GetDecimal(3);
                            product = new Product(pId, name, description, price);
                        }
                    }
                    return product;
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
                        //add all products to new list
                        List<Product> allProducts = new List<Product>();
                        //make reader
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int pid = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string description = reader.GetString(2);
                            decimal price = reader.GetDecimal(3);
                            allProducts.Add(new Product(pid, name, description, price));
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