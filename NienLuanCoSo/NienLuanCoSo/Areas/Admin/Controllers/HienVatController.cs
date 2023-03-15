using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NienLuanCoSo.Areas.Admin.Controllers
{
    public class HienVatController : Controller
    {
        NIENLUANCOSOEntities4 db = new NIENLUANCOSOEntities4();
        // GET: Admin/HienVat
        public ActionResult Index()
        {
            var listHV = from s in db.HIEN_VAT select s;
            return View(listHV);
        }

        // GET: Admin/HienVat/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/HienVat/Create
        public ActionResult Create()
        {
            var loaihienvatlist = db.LOAI_HV.ToList();
            ViewBag.MA_LOAI = new SelectList(loaihienvatlist, dataValueField: "MA_LOAI", dataTextField: "DIEN_GIAI");

            return View();
        }

        // POST: Admin/HienVat/Create
        [HttpPost]
        public ActionResult Create(HIEN_VAT hv)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var loaihienvatlist = db.LOAI_HV.ToList();
                    ViewBag.MA_LOAI = new SelectList(loaihienvatlist, dataValueField: "MA_LOAI", dataTextField: "DIEN_GIAI");
                    if (string.IsNullOrEmpty(hv.TEN_HV) == true)
                    {
                        ModelState.AddModelError("", "Tên hiện vật không được trống!");
                        return View(hv);
                    }
                    if (string.IsNullOrEmpty(hv.DONVITINH) == true)
                    {
                        ModelState.AddModelError("", "Đơn vị tính không được trống!");
                        return View(hv);
                    }
                    if (hv.GIA <= 0 || hv.GIA == null)
                    {
                        ModelState.AddModelError("", "Giá hiện vật không được trống và lớn hơn 0!");
                        return View(hv);
                    }
                    if (hv.SOLUONGCON <= 0 || hv.SOLUONGCON == null)
                    {
                        hv.SOLUONGCON = 0;
                    }
                    HIEN_VAT hv2 = db.HIEN_VAT.SingleOrDefault(s => s.TEN_HV.ToUpper() == hv.TEN_HV.Trim().ToUpper());
                    if (hv2 != null)
                    {
                        ModelState.AddModelError("", "Hiện vật đã tồn tại!");
                        return View(hv);
                    }
                    hv.TEN_HV = hv.TEN_HV.Trim();
                    hv.DONVITINH = hv.DONVITINH.Trim();
                    db.HIEN_VAT.Add(hv);
                    db.SaveChanges();
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Error Save Data");
            }
            //Cập nhật lại danh sách hiển thị
            var listQGHV = from s in db.TT_QUYENGOP_HIENVAT select s;
            return RedirectToAction("index", "HienVat");
        }

        // GET: Admin/HienVat/Edit/5
        public ActionResult Edit(int id)
        {
            var loaihienvatlist = db.LOAI_HV.ToList();
            ViewBag.MA_LOAI = new SelectList(loaihienvatlist, dataValueField: "MA_LOAI", dataTextField: "DIEN_GIAI");
            HIEN_VAT hv = db.HIEN_VAT.Find(id);
            return View(hv);
        }
        // POST: Admin/HienVat/Edit/5
        [HttpPost]
        public ActionResult Edit(HIEN_VAT hv)
        {
            var loaihienvatlist = db.LOAI_HV.ToList();
            ViewBag.MA_LOAI = new SelectList(loaihienvatlist, dataValueField: "MA_LOAI", dataTextField: "DIEN_GIAI");

            try
            {
                if (string.IsNullOrEmpty(hv.TEN_HV) == true)
                {
                    ModelState.AddModelError("", "Tên hiện vật không được trống!");
                    return View(hv);
                }
                if (string.IsNullOrEmpty(hv.DONVITINH) == true)
                {
                    ModelState.AddModelError("", "Đơn vị tính không được trống!");
                    return View(hv);
                }
                if (hv.GIA <= 0 || hv.GIA == null)
                {
                    ModelState.AddModelError("", "Giá hiện vật không được trống và lớn hơn 0!");
                    return View(hv);
                }
                if (hv.SOLUONGCON <= 0 || hv.SOLUONGCON == null)
                {
                    hv.SOLUONGCON = 0;
                }

                HIEN_VAT hv2 = db.HIEN_VAT.SingleOrDefault(s => s.TEN_HV.ToUpper().Trim() == hv.TEN_HV.ToUpper().Trim());
                if (hv2 != null)
                {
                    hv2.TEN_HV = hv2.TEN_HV.Trim().ToUpper();

                    hv.TEN_HV = hv.TEN_HV.Trim().ToUpper();
                    if (hv2.TEN_HV != hv.TEN_HV)
                    {
                        ModelState.AddModelError("", "Hiện vật đã tồn tại!");
                        return View(hv);
                    }
                }
                var hvu = db.HIEN_VAT.Find(hv.MA_HV);
                hvu.TEN_HV = hv.TEN_HV.Trim();
                hvu.DONVITINH = hv.DONVITINH.Trim();
                hvu.GIA = hv.GIA;
                hvu.SOLUONGCON = hv.SOLUONGCON;
                db.Entry(hvu).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Error Save Data");
            }


            return RedirectToAction("Index");

        }

        // GET: Admin/HienVat/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/HienVat/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
