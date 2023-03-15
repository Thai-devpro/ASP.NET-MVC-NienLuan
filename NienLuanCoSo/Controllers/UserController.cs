using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using NienLuanCoSo.Models;

namespace NienLuanCoSo.Controllers
{
    public class UserController : Controller
    {
        NIENLUANCOSOEntities4 db = new NIENLUANCOSOEntities4();
        // GET: User
        public ActionResult Dangki()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Dangki(TAIKHOAN_MTQ tkmtq)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(tkmtq.TAIKHOAN) == true)
                    {
                        ModelState.AddModelError("", "Vui lòng nhập tên tài khoản!");
                        return View(tkmtq);
                    }
                    if (string.IsNullOrEmpty(tkmtq.MATKHAU_MTQ) == true)
                    {
                        ModelState.AddModelError("", "Vui lòng nhập mật khẩu!");
                        return View(tkmtq);
                    }
                    TAIKHOAN_MTQ tkmtqc = db.TAIKHOAN_MTQ.SingleOrDefault(s => s.TAIKHOAN == tkmtq.TAIKHOAN);
                    if(tkmtqc != null)
                    {
                        ModelState.AddModelError("", "Tài khoản đã tồn tại, vui lòng chọn tài khoản khác!");
                        return View(tkmtq);
                    }
                    tkmtq.MA_MTQ = Convert.ToInt32(Session["idmtq"]);

                    db.TAIKHOAN_MTQ.Add(tkmtq);
                    db.SaveChanges();
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Đăng kí thất bại!");
            }
            return RedirectToAction("Thongbao", new { id = tkmtq.MA_MTQ });
        }
        public ActionResult Thongbao(int id)
        {
            MANHTHUONGQUAN ttqg = db.MANHTHUONGQUANs.SingleOrDefault(s => s.MA_MTQ == id);
            return View(ttqg);
        }
        public ActionResult Dangnhap()
        {

            LoginModels ojlogin = new LoginModels();
            return View(ojlogin);
        }

        [HttpPost]

        public ActionResult Dangnhap(FormCollection collection)
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
                TAIKHOAN_MTQ ad = db.TAIKHOAN_MTQ.SingleOrDefault(n => n.TAIKHOAN.Equals(username) && n.MATKHAU_MTQ.Equals(password));
                if (ad != null)
                {
                    ViewBag.Thongbao = "Bạn đã đăng nhập thành công";
                    MANHTHUONGQUAN mtq = db.MANHTHUONGQUANs.Find(ad.MA_MTQ);
                    Session["TaiKhoanmtq"] = mtq.HOTEN_MTQ;
                    Session["idmtq"] = mtq.MA_MTQ;
                    return RedirectToAction("index", "Home");
                }
                else
                    ViewBag.Thongbao = "Tài khoản hoặc mật khẩu không đúng";
            }
            return View();
        }
        public ActionResult Dangxuat()
        {
            Session.Abandon();
            return RedirectToAction("Dangnhap", "User");

        }
        public ActionResult Edit()
        {

            if (Session["Taikhoanmtq"] != null)
            {
                var id = Session["idmtq"];

                MANHTHUONGQUAN mtq = db.MANHTHUONGQUANs.Find(id);
                return View(mtq);
            }
            else
                return HttpNotFound();
        }

        // POST: ManhThuongQuan/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MANHTHUONGQUAN mtq)
        {
            if (Session["Taikhoanmtq"] != null)
            {
                var id = Session["idmtq"];

                try
                {
                    if (string.IsNullOrEmpty(mtq.HOTEN_MTQ) == true)
                    {
                        ModelState.AddModelError("", "Vui lòng nhập họ tên!");
                        return View(mtq);
                    }
                    if (mtq.NGAYSINH_MTQ == DateTime.Now)
                    {
                        ModelState.AddModelError("", "Vui lòng nhập ngày sinh!");
                        return View(mtq);
                    }
                    if (string.IsNullOrEmpty(mtq.DONVI_TOCHUC_MTQ) == true)
                    {
                        ModelState.AddModelError("", "Bạn có đại diện cho tổ chức nào không!");
                        return View(mtq);
                    }
                    string regex = @"^([\+]?33[-]?|[0])?[1-9][0-9]{8}$";
                    if (mtq.SDT_MTQ == null || !Regex.IsMatch(mtq.SDT_MTQ.ToString(), regex))
                    {
                        ModelState.AddModelError("", "Vui lòng nhập số điện thoại chính xác!");
                        return View(mtq);
                    }
                    if (string.IsNullOrEmpty(mtq.DIACHI_MTQ) == true)
                    {
                        ModelState.AddModelError("", "Vui lòng nhập địa chỉ!");
                        return View(mtq);
                    }
                    var mtqUpdate = db.MANHTHUONGQUANs.Find(id);
                    mtqUpdate.HOTEN_MTQ = mtq.HOTEN_MTQ.Trim();
                    mtqUpdate.NGAYSINH_MTQ = mtq.NGAYSINH_MTQ;
                    mtqUpdate.GIOITINH_MTQ = mtq.GIOITINH_MTQ.Trim();
                    mtqUpdate.DONVI_TOCHUC_MTQ = mtq.DONVI_TOCHUC_MTQ.Trim();
                    mtqUpdate.SDT_MTQ = mtq.SDT_MTQ;
                    mtqUpdate.DIACHI_MTQ = mtq.DIACHI_MTQ.Trim();
                    db.Entry(mtqUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Error Save Data");
                }

                return RedirectToAction("Details");
            }
            else
                return HttpNotFound();
        }
        public ActionResult Details()
        {
            if (Session["Taikhoanmtq"] != null)
            {
                var id = Session["idmtq"];

                MANHTHUONGQUAN mtq = db.MANHTHUONGQUANs.Find(id);
                return View(mtq);
            }
            else
                return HttpNotFound();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MANHTHUONGQUAN mtq)
        {
            try
            {

                if (string.IsNullOrEmpty(mtq.HOTEN_MTQ) == true)
                {
                    ModelState.AddModelError("", "Vui lòng nhập họ tên!");
                    return View(mtq);
                }
                if (mtq.NGAYSINH_MTQ == DateTime.Now)
                {
                    ModelState.AddModelError("", "Vui lòng nhập ngày sinh!");
                    return View(mtq);
                }
                if (string.IsNullOrEmpty(mtq.DONVI_TOCHUC_MTQ) == true)
                {
                    ModelState.AddModelError("", "Bạn có đại diện cho tổ chức nào không!");
                    return View(mtq);
                }
                string regex = @"^([\+]?33[-]?|[0])?[1-9][0-9]{8}$";
                if (mtq.SDT_MTQ == null || !Regex.IsMatch(mtq.SDT_MTQ.ToString(), regex))
                {
                    ModelState.AddModelError("", "Vui lòng nhập số điện thoại chính xác!");
                    return View(mtq);
                }

                if (string.IsNullOrEmpty(mtq.DIACHI_MTQ) == true)
                {
                    ModelState.AddModelError("", "Vui lòng nhập địa chỉ!");
                    return View(mtq);
                }
                mtq.HOTEN_MTQ = mtq.HOTEN_MTQ.Trim();
                mtq.DONVI_TOCHUC_MTQ = mtq.DONVI_TOCHUC_MTQ.Trim();
                mtq.DIACHI_MTQ = mtq.DIACHI_MTQ.Trim();
                db.MANHTHUONGQUANs.Add(mtq);
                db.SaveChanges();
                Session["idmtq"] = mtq.MA_MTQ;
                Session["Taikhoanmtq"] = mtq.HOTEN_MTQ;
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Đăng kí thất bại!");
            }
            if (Session["Quyengop"] != null)
            {
                return RedirectToAction("Create", "QGHienVat");
            }
            else if (Session["noihotro"] != null)
            {
                return RedirectToAction("index", "Noihotro");
            }
            else

                return RedirectToAction("Dangki");
        }
    }
}