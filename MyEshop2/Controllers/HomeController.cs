using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEshop2.Controllers
{
    public class HomeController : Controller
    {
        MyEshop2.Models.MyEshop2_DBEntities db = new Models.MyEshop2_DBEntities();
        // GET: Home
        public ActionResult Index()
        {
            var list = db.Products.ToList().Take(6);
            return View(list);
        }
        
        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase upload, string CkEditorFuncNum,string langCode)
        {
            string vImagePath = string.Empty;
            string vMessage = string.Empty;
            string vFilePath = string.Empty;
            string vOutput = string.Empty;

            try
            {
                if (upload != null && upload.ContentLength>0)
                {
                    var vFileName = DateTime.Now.ToString("yyyyMMdd-HHMMssff") +
                        Path.GetExtension(upload.FileName).ToLower();
                    var vFolderPath = Server.MapPath("/Upload/");
                    if (!Directory.Exists(vFolderPath))
                    {
                        Directory.CreateDirectory(vFolderPath);
                    }
                    vFolderPath = Path.Combine(vFolderPath, vFileName);
                    upload.SaveAs(vFilePath);
                    vImagePath = Url.Content("/Upload/" + vFileName);
                    vMessage = "تصویر با موفقیت ذخیره شد";
                }
            }
            catch (Exception)
            {

                vMessage = "مشکلی پیش امد";
            }
            vOutput = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction("")</script>";
            return Content(vOutput);
        }
    }
}