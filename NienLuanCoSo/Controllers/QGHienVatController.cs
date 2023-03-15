using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace NienLuanCoSo.Controllers
{
    public class QGHienVatController : Controller
    {
        NIENLUANCOSOEntities4 db = new NIENLUANCOSOEntities4();
        // GET: QGHienVat
        public ActionResult Index()
        {

            var listQGHV = from s in db.TT_QUYENGOP_HIENVAT select s;
            return View(listQGHV);
        }

        // GET: QGHienVat/Details/5
        public ActionResult Details(int id)
        {
            return null;

        }

        // GET: QGHienVat/Create
        public ActionResult Create(int? id)
        {
            if (Session["Taikhoanmtq"] == null)
            {
                Session["Quyengop"] = "co";
                return RedirectToAction("Create", "User");
            }
            else
            {
                var hienvatlist = db.HIEN_VAT.ToList();
                ViewBag.MA_HV = new SelectList(hienvatlist, dataValueField: "MA_HV", dataTextField: "TEN_HV");
                if (id != null)
                {
                    var ChienDich = db.CHIENDICHes.Where(s => s.MA_CD == id);

                    ViewBag.MA_CD = new SelectList(ChienDich, dataValueField: "MA_CD", dataTextField: "TEN_CD");
                }
                else
                {
                    var ChienDich = db.CHIENDICHes.ToList();

                    ViewBag.MA_CD = new SelectList(ChienDich, dataValueField: "MA_CD", dataTextField: "TEN_CD");
                }
                 return View();
            }
        }

        // POST: QGHienVat/Create
        [HttpPost, ActionName("Create")]
        public ActionResult Create(TT_QUYENGOP_HIENVAT qghv)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var hienvatlist = db.HIEN_VAT.ToList();
                    ViewBag.MA_HV = new SelectList(hienvatlist, dataValueField: "MA_HV", dataTextField: "TEN_HV");
                    var chiendichlist = db.CHIENDICHes.ToList();
                    ViewBag.MA_CD = new SelectList(chiendichlist, dataValueField: "MA_CD", dataTextField: "TEN_CD");
                    if (qghv.SOLUONG_QG == null || qghv.SOLUONG_QG <= 0)
                    {
                        ModelState.AddModelError("", "Vui lòng điền số lượng ?");
                        return View(qghv);
                    }
                    if (string.IsNullOrEmpty(qghv.GHICHU) == true)
                    {
                        ModelState.AddModelError("", "Hãy viết gì đó cho chúng tôi");
                        return View(qghv);
                    }
                    qghv.MA_MTQ = Convert.ToInt32(Session["idmtq"]);
                    qghv.TRANGTHAI_HV = "Chưa nhận";
                    qghv.Ngay_QG = DateTime.Now;
                    db.TT_QUYENGOP_HIENVAT.Add(qghv);
                    db.SaveChanges();
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Error Save Data");
            }
            //Cập nhật lại danh sách hiển thị

            return RedirectToAction("CamOn", new { id = qghv.MA_QGHV });
        }
        public ActionResult CamOn(int id)
        {
            TT_QUYENGOP_HIENVAT ttqg = db.TT_QUYENGOP_HIENVAT.SingleOrDefault(s => s.MA_QGHV == id);
            return View(ttqg);
        }
        public ActionResult DSHV()
        {
            var listHV = from s in db.HIEN_VAT select s;
            return View(listHV);

        }
        // GET: QGHienVat/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QGHienVat/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: QGHienVat/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QGHienVat/Delete/5
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
