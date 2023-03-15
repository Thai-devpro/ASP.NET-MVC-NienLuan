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
    public class ChienDichController : Controller
    {
        NIENLUANCOSOEntities4 db = new NIENLUANCOSOEntities4();
        // GET: Admin/ChienDich
        public ActionResult getChienDich()
        {
            //Xử lý thao tác linq đơn giản
            var listChienDich = from s in db.CHIENDICHes select s;
            return View(listChienDich);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost, ActionName("Create")]
        public ActionResult Create(CHIENDICH cd, HttpPostedFileBase image)
        {
            try
            {
                if (image != null && image.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(image.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/images/chiendich/"), _FileName);
                    image.SaveAs(_path);
                    if (System.IO.File.Exists(_path))
                    {
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        image.SaveAs(_path);
                    }
                    cd.ANH_CD = image.FileName;
                }
                
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(cd.TEN_CD) == true)
                    {
                        ModelState.AddModelError("", "Tên chiến dịch không được trống!");
                        return View(cd);
                    }
                    if (string.IsNullOrEmpty(cd.NOIDUNG_CD) == true)
                    {
                        ModelState.AddModelError("", "Nội dung chiến dịch không được trống!");
                        return View(cd);
                    }
                    if (string.IsNullOrEmpty(cd.ANH_CD) == true)
                    {
                        ModelState.AddModelError("", "Ảnh chiến dịch không được trống!");
                        return View(cd);
                    }
                    if (cd.NGAYBATDAU == null)
                    {
                        ModelState.AddModelError("", "Ngày bắt đầu chiến dịch không được trống!");
                        return View(cd);
                    }
                    if (cd.NGAYKETTHUC == null)
                    {
                        ModelState.AddModelError("", "Ngày kết thúc chiến dịch không được trống!");
                        return View(cd);
                    }
                    if (cd.NGAYKETTHUC <= cd.NGAYBATDAU)
                    {
                        ModelState.AddModelError("", "Ngày kết thúc không được nhỏ hơn ngày bắt đầu!");
                        return View(cd);
                    }

                    cd.TEN_CD = cd.TEN_CD.Trim();
                    cd.NOIDUNG_CD = cd.NOIDUNG_CD.Trim();
                    cd.ANH_CD = cd.ANH_CD.Trim();
                    db.CHIENDICHes.Add(cd);
                    db.SaveChanges();
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Error Save Data");
            }
            //Cập nhật lại danh sách hiển thị
            return Redirect("getChienDich");
        }

        public ActionResult Details(int id)
        {

            return null;

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHIENDICH cd = db.CHIENDICHes.Find(id);
            return View(cd);
        }

        // POST: Admin/QG_HienVat/Edit/5
        [HttpPost]
        public ActionResult Edit(CHIENDICH cd, HttpPostedFileBase image)
        {

            try
            {
                
                if (string.IsNullOrEmpty(cd.TEN_CD) == true)
                {
                    ModelState.AddModelError("", "Tên chiến dịch không được trống!");
                    return View(cd);
                }
                if (string.IsNullOrEmpty(cd.NOIDUNG_CD) == true)
                {
                    ModelState.AddModelError("", "Nội dung chiến dịch không được trống!");
                    return View(cd);
                }
                if (image == null)
                {
                    ModelState.AddModelError("", "Ảnh chiến dịch không được trống!");
                    return View(cd);
                }
                if (cd.NGAYBATDAU == null)
                {
                    ModelState.AddModelError("", "Ngày bắt đầu chiến dịch không được trống!");
                    return View(cd);
                }
                if (cd.NGAYKETTHUC == null)
                {
                    ModelState.AddModelError("", "Ngày kết thúc chiến dịch không được trống!");
                    return View(cd);
                }
                if (cd.NGAYKETTHUC <= cd.NGAYBATDAU)
                {
                    ModelState.AddModelError("", "Ngày kết thúc không được nhỏ hơn ngày bắt đầu!");
                    return View(cd);
                }
                if (image != null && image.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(image.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/images/chiendich/"), _FileName);
                    image.SaveAs(_path);
                    if (System.IO.File.Exists(_path))
                    {
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        image.SaveAs(_path);
                    }
                    cd.ANH_CD = image.FileName;
                }

                CHIENDICH cdu = db.CHIENDICHes.SingleOrDefault(s => s.MA_CD == cd.MA_CD);
                cdu.TEN_CD = cd.TEN_CD.Trim();
                cdu.NOIDUNG_CD = cd.NOIDUNG_CD.Trim();
                cdu.ANH_CD = cd.ANH_CD.Trim();
                cdu.NGAYBATDAU = cd.NGAYBATDAU;
                cdu.NGAYKETTHUC = cd.NGAYKETTHUC;
                db.Entry(cdu).State = EntityState.Modified;
                db.SaveChanges();
                
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Error Save Data");
            }


            return RedirectToAction("getChienDich");

        }

        // GET: Admin/QG_HienVat/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHIENDICH cd = db.CHIENDICHes.Find(id);
            return View(cd);
        }

        // POST: Admin/QG_HienVat/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            CHIENDICH cd = db.CHIENDICHes.Find(id);

            var tt = db.TT_TRAOTANG.Where(n => n.MA_CD == id).ToList();
            //var hk = db.TT_QUYENGOP_HIENKIM.Where(n => n.MA_CD == id).ToList();
            var hv = db.TT_QUYENGOP_HIENVAT.Where(n => n.MA_CD == id).ToList();
            if (cd == null)
            {
                Response.StatusCode = 404;
                return View(HttpNotFound());
            }
            else if (tt == null && hv == null)
            {

                db.CHIENDICHes.Remove(cd);
                db.SaveChanges();
                return RedirectToAction("getChienDich");
            }
            else
            {
                ViewBag.erro = "Không thể xóa chiến dịch đã có quyên góp hoặc trao tặng!";
                return RedirectToAction("Delete");
            }
        }


    }
}