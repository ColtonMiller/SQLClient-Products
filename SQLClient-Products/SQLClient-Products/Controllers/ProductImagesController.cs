using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQLClient_Products.Controllers
{
    public class ProductImagesController : Controller
    {
        // GET: ProductImage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }
    }
}