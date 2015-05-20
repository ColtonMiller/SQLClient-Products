using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SQLClient_Products.Models;

namespace SQLClient_Products.Controllers
{
    public class ProductImagesController : Controller
    {
        // GET: ProductImage
        public ActionResult Index()
        {
            return View(ProductImageRepository.GetAllProductImages());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View(new ProductImage());
        }
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file, ProductImage productImage)
        {
                // get the filename
                var filename = Guid.NewGuid().ToString().Substring(0, 10) + "_" + Path.GetFileName(file.FileName);
                //get filename path
                var filenamePath = Path.Combine(Server.MapPath("~/Content/Uploads"), filename);
                //save the file
                file.SaveAs(filenamePath);
                productImage.ImageUrl = "Content/Uploads/" + filename;
                //tell user file was uploaded
                ViewBag.Message = "File uploaded";
                if (ProductImageRepository.InsertProductImage(productImage.ImageUrl,productImage.ProductId))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.message = "Failed to insert record";
                    return RedirectToAction("Index");
                }
            
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            ProductImage image = new ProductImage();
            image.ProductImageId = id;
            return View(image);
        }
        //TODO: Create DeleteConfirmation action
        //The GET action will accept an integer Id as an arguement and delete the product from the database.  After the deletion is complete, redirect the user to the Index (listing) action.
        [HttpGet]
        public ActionResult DeleteConfirmation(int id)
        {
            ProductImageRepository.DeleteProductImage(id);
            return RedirectToAction("Index");
        }
    }
}