using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SQLClient_Products.Models;
using System.IO;

namespace SQLClient_Products.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            //return a list of products to the view.  The view will display a table of all products, with links to edit or delete the product.
            return View(ProductRepository.GetAllProducts()); 
        }

        //TODO: Create Create actions.  
        //The GET action will take no arguments and pass an empty Product object to the View and display the Create form to the user.  
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Product());
        }
        //The POST action will accept a Product object as an argument, handle the uploading of an image file, then add it to the database.
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file, Product product)
        {
            
                // get the filename
                var fileName = Guid.NewGuid().ToString().Substring(0, 10) + "_" + Path.GetFileName(file.FileName);
                //get filename path
                var fileNamePath = Path.Combine(Server.MapPath("~/Content/Uploads/"), fileName);
                //save the file
                file.SaveAs(fileNamePath);
                //tell user file was uploaded
                ViewBag.Message = "File uploaded";
                if (ProductRepository.InsertProduct(product.Name, product.Description, product.Price, fileNamePath))
                {
                    return RedirectToAction("Index"); 
                }
                else
                {
                    ViewBag.message = "Failed to insert record";
                    return RedirectToAction("Index"); 
                }
            
        }
        //TODO: Create Edit actions.  
        //The GET action will accept an integer Id as an arguement.  The action will retrieve the product from the database, and pass it to the view to display the Edit form to the user, with the field values populated from the database.
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Product product = ProductRepository.GetProductById(id);
            return View(product);
        }
        //The POST action will accept an integer Id and a product object as arguements.  The action will then upload a new file if one was selected, then update the record in the database.
        [HttpPost]
        public ActionResult Edit(int id, Product product)
        {
            if (ProductRepository.UpdateProduct(id,product.Name,product.Description,product.Price,product.ImageUrl))
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Could not update";
                return View(product);
            }
        }
        //TODO: Create Delete action
        //The GET action will accept an integer Id as an arguement and retrieve the product from the database.  The product object will be passed to the view to display to the user a confirmation screen with a button to confirm that links to the DeleteConfirmation action.
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Product product = ProductRepository.GetProductById(id);
            return RedirectToAction("Delete");
        }
        //TODO: Create DeleteConfirmation action
        //The GET action will accept an integer Id as an arguement and delete the product from the database.  After the deletion is complete, redirect the user to the Index (listing) action.
        [HttpGet]
        public ActionResult DeleteConfirmation(int id)
        {
            ProductRepository.DeleteProduct(id);
            return RedirectToAction("Index");
        }

    }
}