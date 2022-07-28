using MyEshop2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MyEshop2.Controllers
{
    public class ShopController : ApiController
    {
        // GET: api/Shop
        public string Get()
        {
            int Count = 0;
            var Sessions = HttpContext.Current.Session;
            List<ShopCartItem> ShopCart = new List<ShopCartItem>();
            if (Sessions["ShoppingCart"] != null)
            {
                ShopCart = Sessions["ShoppingCart"] as List<ShopCartItem>;
                Count = ShopCart.Sum(s => s.ProductCount);
                }

                return "سبد خرید شما:" + Count + " "+ "کالا";
        }

        // GET: api/Shop/5
        public string Get(int Productid)
        {
            var Sessions = HttpContext.Current.Session;
            List<ShopCartItem> ShopCart = new List<ShopCartItem>();
            if(Sessions["ShoppingCart"]!= null)
            {
                ShopCart = Sessions["ShoppingCart"] as List<ShopCartItem>;
                if (ShopCart.Any(s=>s.ProductID==Productid))
                {
                    int index = ShopCart.FindIndex(s => s.ProductID == Productid);
                    ShopCart[index].ProductCount += 1;
                }

            }
            else
            {
                ShopCart.Add(new ShopCartItem()
                {
                    ProductID=Productid,
                    ProductCount=1

                });
            }
            Sessions["ShoppingCart"] = ShopCart;
            return "value";
        }

        // POST: api/Shop
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Shop/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Shop/5
        public void Delete(int id)
        {
        }
    }
}
