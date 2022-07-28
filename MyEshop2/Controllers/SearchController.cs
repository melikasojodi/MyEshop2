using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEshop2.Controllers
{
    public class SearchController : Controller
    {
        MyEshop2.Models.MyEshop2_DBEntities db = new Models.MyEshop2_DBEntities();
        // GET: Search
        public ActionResult Index(string Search)
        {
            if (Search != null)
            {
                var list = db.Products.Where(p => p.ProductTitle.Contains(Search) || p.ProductDescription.Contains(Search)).ToList();
                return View(list.Distinct()); //برای جلوگیزی از نمایش محصولات تکراری
            }
            else
            {
                var ShowAll = db.Products.ToList();
                return View(ShowAll);
            }

        }
        public ActionResult Tag(string id)
        {
            var list = db.Products.Where(p => p.ProductTitle.Contains(id) || p.ProductDescription.Contains(id)).ToList();
            return View(list.Distinct()); //برای جلوگیزی از نمایش محصولات تکراری
        }
    }
}