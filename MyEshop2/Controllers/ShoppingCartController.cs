using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyEshop2.Models;

namespace MyEshop2.Controllers
{
    public class ShoppingCartController : Controller
    {
        MyEshop2_DBEntities db = new MyEshop2_DBEntities();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            List<ShowShoppingCartcs> ShopCart = new List<ShowShoppingCartcs>();
            if (Session["ShoppingCart"] != null)
            {
                List<ShopCartItem> shop = Session["ShoppingCart"] as List<ShopCartItem>;
                foreach (var item in shop)
                {
                    var Product = db.Products.Find(item.ProductID);
                    ShopCart.Add(new ShowShoppingCartcs()
                    {
                        ProductCount=item.ProductCount,
                        ProductID=item.ProductID,
                        ProductPrice=Product.ProductPrice,
                        ProductTitle=Product.ProductTitle,
                        Sum=item.ProductCount * Product.ProductPrice

                    });
                }
            }
            return View(ShopCart);
        }
        public ActionResult CountUp(int id)
        {
            List<ShopCartItem> shop = Session["ShoppingCart"] as List<ShopCartItem>;
            int index = shop.FindIndex(s => s.ProductID == id);
            shop[index].ProductCount += 1;
            Session["ShoppingCart"] = shop;
            return RedirectToAction("Index");

        }
        public ActionResult CountDown(int id)
        {
            List<ShopCartItem> shop = Session["ShoppingCart"] as List<ShopCartItem>;
            int index = shop.FindIndex(s => s.ProductID == id);
            shop[index].ProductCount -= 1;

            if (shop[index].ProductCount == 0)
            {
                shop.Remove(shop[index]);
            }
            Session["ShoppingCart"] = shop;
            return RedirectToAction("Index");

        } 
        public ActionResult Remove(int id)
        {
            List<ShopCartItem> shop = Session["ShoppingCart"] as List<ShopCartItem>;
            int index = shop.FindIndex(s => s.ProductID == id);
            
                shop.Remove(shop[index]);
           
            Session["ShoppingCart"] = shop;
            return RedirectToAction("Index");

        }
        [Authorize]
        public ActionResult Save()
        {
            List<ShopCartItem> shop = Session["ShoppingCart"] as List<ShopCartItem>;
            int UserId = db.Users.First(u => u.Username == User.Identity.Name).UserID;
            Orders order = new Orders()
            {
                Date = DateTime.Now,
                IsFinally=false
            };
            db.Orders.Add(order);
            foreach (var item in shop)
            {
                var Product = db.Products.Find(item.ProductID);
                db.OrderDetails.Add(new OrderDetails() { 
                
                    OrderId=order.OrderId,
                    ProductCount=item.ProductCount,
                    ProductPrice=Product.ProductPrice,
                    ProductId=Product.ProductID,
                    Sum=item.ProductCount * Product.ProductPrice
                });

            }
            db.SaveChanges();
            return null;
        }
    }
}