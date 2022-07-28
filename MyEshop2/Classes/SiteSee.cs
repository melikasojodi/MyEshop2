using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEshop2.Classes
{
    public class SiteSee
    {
        public static void see()
        {
            using(MyEshop2.Models.MyEshop2_DBEntities db= new Models.MyEshop2_DBEntities())
            {
                DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                var state = db.StateSite.FirstOrDefault(s => s.Date == dt);
                int count = 0;

                if (state != null)
                {
                    state.Count += 1;
                }
                else
                {
                    db.StateSite.Add(new Models.StateSite()
                    {
                        Date = dt,
                        Count=1,

                    }) ;
                   
                    
                }
                db.SaveChanges();
            }
        }
    }
}