using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NienLuanCoSo.Areas.Admin.Controllers
{
    public class LoaiHienVatController : Controller
    {
        NIENLUANCOSOEntities4 db = new NIENLUANCOSOEntities4();
        // GET: Admin/LoaiHienVat
        public ActionResult Index()
        {
            var listLHV = from s in db.LOAI_HV select s;
            return View(listLHV);
           
        }

        // GET: Admin/LoaiHienVat/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/LoaiHienVat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiHienVat/Create
        [HttpPost]
        public ActionResult Create(LOAI_HV lhv)
        {

     

            try
            {
                if (string.IsNullOrEmpty(lhv.DIEN_GIAI) == true)
                {
                    ModelState.AddModelError("", "Loại hiện vật không được trống!");
                    return View(lhv); 
                }
     
                
                
                lhv.DIEN_GIAI = lhv.DIEN_GIAI.Trim();
                LOAI_HV lhvc = db.LOAI_HV.SingleOrDefault(s => s.DIEN_GIAI.ToUpper() == lhv.DIEN_GIAI.ToUpper());
                if (lhvc != null)
                {
                    ModelState.AddModelError("", "Loại hiện vật đã tồn tại!");
                    return View(lhv);
                }
                db.LOAI_HV.Add(lhv);
                    db.SaveChanges();
                
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Error Save Data");
            }
            //Cập nhật lại danh sách hiển thị
            return Redirect("index");
        }

        // GET: Admin/LoaiHienVat/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAI_HV lhv = db.LOAI_HV.Find(id);
            return View(lhv);
        }

        // POST: Admin/LoaiHienVat/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(LOAI_HV lhv)
        { 
            {
                try
                {
                    if (string.IsNullOrEmpty(lhv.DIEN_GIAI) == true)
                    {
                        ModelState.AddModelError("", "Loại hiện vật không được trống!");
                        return View(lhv);
                    }
                    LOAI_HV lhvc2 = db.LOAI_HV.SingleOrDefault(s => s.DIEN_GIAI.ToUpper() == lhv.DIEN_GIAI.ToUpper());
                    if (lhvc2 != null)
                    {
                        ModelState.AddModelError("", "Loại hiện vật đã tồn tại!");
                        return View(lhv);
                    }
                    LOAI_HV lhvc = db.LOAI_HV.Find(lhv.MA_LOAI);
                    lhvc.DIEN_GIAI = lhv.DIEN_GIAI.Trim();
                    if (lhvc.DIEN_GIAI.ToUpper() == lhv.DIEN_GIAI.ToUpper())
                    {
                        ModelState.AddModelError("", "Loại hiện vật đã tồn tại!");
                        return View(lhv);
                    }
                    db.Entry(lhvc).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Error Save Data");
                }
            }

            return RedirectToAction("index");
        }

        // GET: Admin/LoaiHienVat/Delete/5
        public ActionResult Delete(int id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAI_HV lhv = db.LOAI_HV.SingleOrDefault(s =>s.MA_LOAI == id);
            
         
            
            return View(lhv);
        }

        // POST: Admin/LoaiHienVat/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete2(int id)
        {
            
            LOAI_HV lhv = db.LOAI_HV.SingleOrDefault(n => n.MA_LOAI == id);


            try
            {
                HIEN_VAT hv = db.HIEN_VAT.SingleOrDefault(n => n.MA_LOAI == lhv.MA_LOAI);
                if (hv != null)
                {
                    ViewBag.Loi = "Loại hiện vật đã có hiện vật";
                    return View("Delete");
                    
                }
                {
                    db.LOAI_HV.Remove(lhv);
                    db.SaveChanges();
                }
            }
            catch
            {
                ViewBag.Loi = "Không thể xóa";
                return View("Delete");
            }
            return RedirectToAction("Index");
            
        }
    }
}
