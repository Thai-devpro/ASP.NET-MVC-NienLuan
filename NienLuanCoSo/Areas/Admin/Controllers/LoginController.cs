using NienLuanCoSo.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NienLuanCoSo.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        public object SessionHelper { get; private set; }

        NIENLUANCOSOEntities4 db = new NIENLUANCOSOEntities4();
        // GET: Admin/Login
        [HttpGet]
        public ActionResult login()
        {
            LoginModel ojlogin = new LoginModel();
            return View(ojlogin);
        }

        [HttpPost]
        public ActionResult login(FormCollection collection)
        {
            var username = collection["username"];
            var password = collection["password"];
            if (string.IsNullOrEmpty(username))
            {
                ViewData["Loi1"] = "Nhập tài khoản";
            }
            else if (string.IsNullOrEmpty(password))
            {
                ViewData["Loi2"] = "Nhập mật khẩu";
            }
            else
            {
                TAIKHOAN_ADMIN ad = db.TAIKHOAN_ADMIN.SingleOrDefault(n => n.TAIKHOAN.Equals(username) && n.MATKHAU.Equals(password));
                if (ad != null)
                {
                    ViewBag.Thongbao = "Bạn đã đăng nhập thành công";
                    Session["TaiKhoan"] = ad;
                    return RedirectToAction("index", "Home");
                }
                else
                    ViewBag.Thongbao = "Tài khoản hoặc mật khẩu không đúng";
            }
            return View();
        }


    }
}