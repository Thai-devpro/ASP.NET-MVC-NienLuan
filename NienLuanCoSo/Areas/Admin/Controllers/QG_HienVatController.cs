using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NienLuanCoSo.Areas.Admin.Controllers
{
    public class QG_HienVatController : Controller
    {
        NIENLUANCOSOEntities4 db = new NIENLUANCOSOEntities4();
        // GET: Admin/QG_HienVat
        public ActionResult Index(string Search ="")
        {
            if (Search != "")
            {
                int tk = int.Parse(Search);
               
                var tt = db.TT_QUYENGOP_HIENVAT.Where(s => s.MA_QGHV == tk);
                return View(tt.ToList());
            }
            var listChienDich = from s in db.TT_QUYENGOP_HIENVAT /*where s.TRANGTHAI_HV =="Đã nhận"*/ select s;
            return View(listChienDich);
        }

        // GET: Admin/QG_HienVat/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/QG_HienVat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/QG_HienVat/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/QG_HienVat/Edit/5
        public ActionResult Edit(int id)
        {
            if (id != null)
            { 

                TT_QUYENGOP_HIENVAT mtq = db.TT_QUYENGOP_HIENVAT.Find(id);
                return View(mtq);
            }
            else
                return HttpNotFound();
        }

        // POST: Admin/QG_HienVat/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TT_QUYENGOP_HIENVAT tt)
        {


            try
            {
               
                
                TT_QUYENGOP_HIENVAT ttu = db.TT_QUYENGOP_HIENVAT.SingleOrDefault(s => s.MA_QGHV == tt.MA_QGHV);
                ttu.TRANGTHAI_HV = tt.TRANGTHAI_HV;
                
                db.Entry(ttu).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Error Save Data");
            }

            return RedirectToAction("Index");



        }

        // GET: Admin/QG_HienVat/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/QG_HienVat/Delete/5
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
