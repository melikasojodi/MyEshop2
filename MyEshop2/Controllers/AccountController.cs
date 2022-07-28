using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyEshop2.Models;
using CaptchaMvc.HtmlHelpers;
using System.Web.Security;

namespace MyEshop2.Controllers
{
    public class AccountController : Controller
    {
        MyEshop2_DBEntities database = new MyEshop2_DBEntities();

        public string ActiveCode { get; private set; }

        // GET: Account
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                if (this.IsCaptchaValid("کد تصویر امنیتی وارد شده اشتباه است"))
                {
                    Users user = new Users()
                    {
                        Username = register.UserName.Trim().ToLower(),
                        Password = register.Pass,
                        Email = register.Email.Trim().ToLower(),
                        RoleID = 2,
                        CreateDate = DateTime.Now,
                        ActiveCode = Guid.NewGuid().ToString().Replace("-", ""),
                        IsActive = false


                    };
                    database.Users.Add(user);
                    database.SaveChanges();
                    return View("RegisterSuccess");
                }

                else
                {
                    ModelState.AddModelError("CaptchaInputText", "کد تصویر امنیتی وارد شده اشتباه است");
                    return View(register);
                }


            }
            return View(register);

        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginViewModel login , string ReturnUrl="")
        {
            if (ModelState.IsValid)
            {
                var Qlogin = database.Users.FirstOrDefault(u => u.Username == login.UserName.Trim().ToLower() && u.Password == login.password);
                if (Qlogin != null)
                {
                    if(Qlogin.IsActive)
                    {
                        FormsAuthentication.SetAuthCookie(login.UserName, login.Remember);
                        if (ReturnUrl != "")
                        {
                            return Redirect(ReturnUrl);
                        }
                         return Redirect("/");
                    }
                    else
                    {
                        ModelState.AddModelError("username", "حساب کاربری فوق غیر فعال است");

                    }

                }
                else
                {
                    ModelState.AddModelError("username", "کاربری یافت نشد");
                }
            }
            return View(login);
        }

        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

        public ActionResult ActiveUser(string Active_Code)
        {
            var user = database.Users.FirstOrDefault(u => u.ActiveCode == Active_Code);
            if (user !=null)
            {
                user.IsActive = true;
                user.ActiveCode = Guid.NewGuid().ToString().Replace("-", "");
                database.SaveChanges();
                ViewBag.ok = true;
            }
            else
            {
                ViewBag.ok = false;
            }
            return View();
        }
    }
}