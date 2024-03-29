﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyEshop2.Models;

namespace MyEshop2.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private MyEshop2_DBEntities db = new MyEshop2_DBEntities();

        public object InsertShowImage { get; private set; }

        // GET: Admin/Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Product_Groups);
            return View(products.ToList());
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(db.Product_Groups, "GroupID", "GroupTitle");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Products products,HttpPostedFileBase ProductImage, HttpPostedFileBase[] ProductGallery, string tags)
        {
            if (ModelState.IsValid)
            {
                string imagename = "no-image.jpg";
                if (ProductImage !=null)
                {
                    imagename = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(ProductImage.FileName);
                    ProductImage.SaveAs(Server.MapPath("/ProductImages/Image/" + imagename));
                    //resizeimage
                }
                products.ProductImage = imagename;
                products.CreateDate = DateTime.Now;
                db.Products.Add(products);

                if (ProductGallery != null && ProductGallery.Any())
                {
                    foreach (HttpPostedFileBase file in ProductGallery)
                    {
                        string galleryname = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                        file.SaveAs(Server.MapPath("/ProductImages/Image/" + galleryname));
                        db.Product_Galleries.Add(new Product_Galleries()
                        {
                            ImageName = galleryname,
                            ProductID = products.ProductID
                        });
                    }
                }



                if (!string.IsNullOrEmpty(tags))
                {
                    string[] tag = tags.Split('-');
                    foreach (string t in tag)
                    {
                        db.Product_Tags.Add(new Product_Tags()
                        {
                            ProductID = products.ProductID,
                            TagTitle = t.Trim()
                        });
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(db.Product_Groups, "GroupID", "GroupTitle", products.GroupID);
            return View(products);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(db.Product_Groups, "GroupID", "GroupTitle", products.GroupID);
            string tags = "";
            foreach (var t in products.Product_Tags)
            {
                tags += t.TagTitle + "-";
            }
            if (tags.EndsWith("-"))
                tags = tags.Substring(0, tags.Length - 1);
            ViewBag.tag = tags;
            return View(products);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Products products, HttpPostedFileBase ProductImage, HttpPostedFileBase[] ProductGallery, string tags)
        {
            if (ModelState.IsValid)
            {
                if (ProductImage != null)
                {
                    if (products.ProductImage != "no-image.jpg")
                    {
                        if (System.IO.File.Exists(Server.MapPath("/ProductImages/Image/" + products.ProductImage)))

                            System.IO.File.Delete(Server.MapPath("/ProductImages/Image/" + products.ProductImage));

                    }
                    products.ProductImage = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(ProductImage.FileName);
                    ProductImage.SaveAs(Server.MapPath("/ProductImages/Image" + products.ProductImage));

                }

                if (ProductGallery != null && ProductGallery.Any())
                {
                    foreach (HttpPostedFileBase file in ProductGallery)
                    {
                        string galleryname = Guid.NewGuid().ToString().Replace("-","") + Path.GetExtension(file.FileName);
                        file.SaveAs(Server.MapPath("/ProductImages/Image/" + galleryname));
                        db.Product_Galleries.Add(new Product_Galleries()
                        {
                            ImageName = galleryname,
                            ProductID = products.ProductID
                        });
                    }
                }

                db.Product_Tags.Where(t => t.ProductID == products.ProductID).ToList().ForEach(t => db.Product_Tags.Remove(t));
                if (!string.IsNullOrEmpty(tags))
                {
                    string[] tag = tags.Split('-');
                    foreach (string t in tag)
                    {
                        db.Product_Tags.Add(new Product_Tags()
                        {
                            ProductID = products.ProductID,
                            TagTitle = t.Trim()
                        });
                    }
                }
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(db.Product_Groups, "GroupID", "GroupTitle", products.GroupID);
            return View(products);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.Products.Find(id);

            foreach (var gallery in products.Product_Galleries.ToList())
            {
              
                if (System.IO.File.Exists(Server.MapPath("/ProductImages/Image/" + gallery.ImageName)))

                    System.IO.File.Delete(Server.MapPath("/ProductImages/Image/" + gallery.ImageName));




                db.Product_Galleries.Remove(gallery);
            }

            db.Product_Tags.Where(t => t.ProductID == products.ProductID).ToList().ForEach(t => db.Product_Tags.Remove(t));

            if (products.ProductImage != "no-image.jpg")
            {

                if (System.IO.File.Exists(Server.MapPath("/ProductImages/Image/" + products.ProductImage)))

                    System.IO.File.Delete(Server.MapPath("/ProductImages/Image/" + products.ProductImage));

            }
            db.Products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public bool DeleteGallery(int id)
        {
            var gallery = db.Product_Galleries.Find(id);
                if(System.IO.File.Exists(Server.MapPath("/ProductImages/Image/"+gallery.ImageName)))
            
                System.IO.File.Delete(Server.MapPath("/ProductImages/Image/" + gallery.ImageName));

               
                
            
            db.Product_Galleries.Remove(gallery);
            db.SaveChanges();
            return true;

        }
    }
}
