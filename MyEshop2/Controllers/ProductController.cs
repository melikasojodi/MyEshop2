using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyEshop2.Models;

namespace MyEshop2.Controllers
{
    public class ProductController : Controller
    {
        MyEshop2_DBEntities database = new MyEshop2_DBEntities();
        // GET: Product
        public ActionResult ShowGroup()
        {
            return PartialView(database.Product_Groups.ToList());
        }
        public ActionResult ShowProductByGroup(int id)
        {
            return View(database.Products.Where(p => p.GroupID == id).ToList());
        }

        public ActionResult ShowProduct(int id)
        {
            return View(database.Products.Find(id));
        }
    }
}