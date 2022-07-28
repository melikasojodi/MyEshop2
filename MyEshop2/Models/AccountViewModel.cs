using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyEshop2.Models
{
    public class RegisterViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]

        public string UserName { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Pass { get; set; }

        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("Pass", ErrorMessage = "کلمه عبور با هم مغایرت دارند")]

        public string repass { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        //[DataType(DataType.EmailAddress,ErrorMessage ="ایمیل وارد شده نا معتبر است")]
        [EmailAddress(ErrorMessage = "{0} وارد شده نا معتبر است ")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }


        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string password { get; set; }


        [Display(Name = "مرا به خاطر بسپار")]
        public bool Remember { get; set; }
    }
}