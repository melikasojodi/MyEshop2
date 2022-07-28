using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyEshop2.Models
{
    public class ShowShoppingCartcs
    {
        [Key]
        public int ProductID { get; set; }

        [Display(Name ="نام محصول")]
        public string ProductTitle { get; set; }

        [Display(Name = "تعداد")]
        public int ProductCount { get; set; }

        [Display(Name = "مبلغ ")]
        public int ProductPrice { get; set; }

        [Display(Name = "جمع کل ")]
        public int Sum { get; set; }
    }
}