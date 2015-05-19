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
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }

        //empty constructor
        public ProductImage() { }

        //constructor that takes in all four arguments 
        public ProductImage(int productImageId, int productId, string imageURL)
        {
            this.ProductImageId = productImageId;
            this.ProductId = productId;
            this.ImageUrl = imageURL;
        }
    }
}