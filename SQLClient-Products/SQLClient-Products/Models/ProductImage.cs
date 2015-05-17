using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQLClient_Products.Models
{
    public class ProductImage
    {
        //make properties
        public int ProductImageId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ProductId { get; set; }

        //empty constructor
        public ProductImage() { }

        //constructor that takes in all four arguments 
        public ProductImage(int productImageId, string title, string description, int productId)
        {
            this.ProductImageId = productImageId;
            this.Title = title;
            this.Description = description;
            this.ProductId = productId;
        }
    }
}