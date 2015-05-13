using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQLClient_Products.Models
{
    public class Product
    {
        //properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price {get; set;}
        public string ImageUrl { get; set; }

        //empty constructor 
        public Product() { }
        //constructor that takes in properties
        public Product(int id, string name, string description, decimal price, string imageUrl)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.ImageUrl = imageUrl;
        } 
        //TODO: fill in the product class. 
        // It should have at least the following properties:
        //     Id, Name, Description, Price, ImageUrl
    }
}