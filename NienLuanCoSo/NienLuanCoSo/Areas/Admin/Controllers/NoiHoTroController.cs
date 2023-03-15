using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NienLuanCoSo.Areas.Admin.Controllers
{
    public class NoiHoTroController : Controller
    {
        NIENLUANCOSOEntities4 db = new NIENLUANCOSOEntities4();
        // GET: Admin/NoiHoTro
        public ActionResult Index()
        {
            var listNHT = from s in db.NOIHOTROes /*where s.TRANGTHAI_NHT=="Đã duyệt"*/ select s;
            return View(listNHT);
        }

        // GET: Admin/NoiHoTro/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/NoiHoTro/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NoiHoTro/Create
        // POST: Noihotro/Create
        [HttpPost, ActionName("Create")]
        [ValidateInput(false)]
        public ActionResult CreateBook(NOIHOTRO nht, HttpPostedFileBase image)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (string.IsNullOrEmpty(nht.CANHOTRO) == true)
                    {
                        ModelState.AddModelError("", "Bạn cần hỗ trợ gì?");
                        return View(nht);
                    }
                    if (string.IsNullOrEmpty(nht.DIACHI) == true)
                    {
                        ModelState.AddModelError("", "Vui lòng nhập địa chỉ!");
                        return View(nht);
                    }
                    if (string.IsNullOrEmpty(nht.TINHTRANG) == true)
                    {
                        ModelState.AddModelError("", "Vui lòng nhập tình trạng!");
                        return View(nht);
                    }
                    if (image != null && image.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(image.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/images/noihotro/"), _FileName);
                        image.SaveAs(_path);
                        nht.ANH_NTH = image.FileName;
                    }
                    if (string.IsNullOrEmpty(nht.ANH_NTH) == true)
                    {
                        ModelState.AddModelError("", "Vui lòng chọn ảnh làm minh chứng!");
                        return View(nht);
                    }


                    nht.MA__MTQ = 31;
                    nht.DIACHI = nht.DIACHI.Trim();
                    nht.CANHOTRO = nht.CANHOTRO.Trim();
                    nht.TINHTRANG = nht.TINHTRANG.Trim();
                    nht.TRANGTHAI_NHT = nht.TRANGTHAI_NHT;
                    db.NOIHOTROes.Add(nht);
                    db.SaveChanges();

                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Error Save Data");
            }
            //Cập nhật lại danh sách hiển thị
            var listnth = from s in db.NOIHOTROes select s;
            return RedirectToAction("index", "Noihotro");
        }

        // GET: Admin/NoiHoTro/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NOIHOTRO nht = db.NOIHOTROes.SingleOrDefault(s => s.MANOI == id);
            return View(nht);
        }

        // POST: Admin/NoiHoTro/Edit/5
        [HttpPost]
        public ActionResult Edit(NOIHOTRO nht, HttpPostedFileBase image)
        {
            NOIHOTRO nht2 = db.NOIHOTROes.SingleOrDefault(s => s.MANOI == nht.MANOI);
            if (nht2.MA__MTQ != 31)
            {
                try
                {
                    if (image != null && image.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(image.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/images/noihotro/"), _FileName);
                        image.SaveAs(_path);
                        nht.ANH_NTH = image.FileName;
                    }
                    if (string.IsNullOrEmpty(nht.CANHOTRO) == true)
                    {
                        ModelState.AddModelError("", "Bạn cần hỗ trợ gì?");
                        return View(nht);
                    }
                    if (string.IsNullOrEmpty(nht.DIACHI) == true)
                    {
                        ModelState.AddModelError("", "Vui lòng nhập địa chỉ!");
                        return View(nht);
                    }
                    if (string.IsNullOrEmpty(nht.TINHTRANG) == true)
                    {
                        ModelState.AddModelError("", "Vui lòng nhập tình trạng!");
                        return View(nht);
                    }

                    if (nht2.ANH_NTH == null)
                    {
                        ModelState.AddModelError("", "Vui lòng chọn ảnh làm minh chứng!");
                        return View(nht);
                    }
                    NOIHOTRO nhtu = db.NOIHOTROes.SingleOrDefault(s => s.MANOI == nht.MANOI);
                    nhtu.MA__MTQ = nht2.MA__MTQ;
                    nhtu.DIACHI = nht2.DIACHI.Trim();
                    nhtu.CANHOTRO = nht2.CANHOTRO.Trim();
                    nhtu.TINHTRANG = nht2.TINHTRANG.Trim();
                    nhtu.TRANGTHAI_NHT = nht.TRANGTHAI_NHT.Trim();
                    nhtu.ANH_NTH = nht2.ANH_NTH;
                    db.Entry(nhtu).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Error Save Data");
                }

                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    if (image != null && image.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(image.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/images/noihotro/"), _FileName);
                        image.SaveAs(_path);
                        nht.ANH_NTH = image.FileName;
                    }
                    NOIHOTRO nhtu = db.NOIHOTROes.SingleOrDefault(s => s.MANOI == nht.MANOI);
                    if (string.IsNullOrEmpty(nht.CANHOTRO) == true)
                    {
                        ModelState.AddModelError("", "Bạn cần hỗ trợ gì?");
                        return View(nht);
                    }
                    if (string.IsNullOrEmpty(nht.DIACHI) == true)
                    {
                        ModelState.AddModelError("", "Vui lòng nhập địa chỉ!");
                        return View(nht);
                    }
                    if (string.IsNullOrEmpty(nht.TINHTRANG) == true)
                    {
                        ModelState.AddModelError("", "Vui lòng nhập tình trạng!");
                        return View(nht);
                    }

                    if (nht.ANH_NTH != null)
                        nhtu.ANH_NTH = nht.ANH_NTH;
                    if (nht.ANH_NTH == null)
                        nhtu.ANH_NTH = nhtu.ANH_NTH;
                    


                    nhtu.DIACHI = nht.DIACHI.Trim();
                    nhtu.CANHOTRO = nht.CANHOTRO.Trim();
                    nhtu.TINHTRANG = nht.TINHTRANG.Trim();
                    nhtu.TRANGTHAI_NHT = nht.TRANGTHAI_NHT.Trim();
                    
                    db.Entry(nhtu).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Error Save Data");
                }

                return RedirectToAction("Index");
            }

        }

        // GET: Admin/NoiHoTro/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NOIHOTRO nht = db.NOIHOTROes.SingleOrDefault(s => s.MANOI == id);
            return View(nht);
        }

        // POST: Noihotro/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult Deleteconfirm(int? id)
        {
            NOIHOTRO nht = db.NOIHOTROes.SingleOrDefault(n => n.MANOI == id);
            var path = Path.Combine(Server.MapPath("~/Content/images/noihotro"), nht.ANH_NTH);

            if (nht == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Xoá ảnh trong thư mục ~/Content/Image
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            db.NOIHOTROes.Remove(nht);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
