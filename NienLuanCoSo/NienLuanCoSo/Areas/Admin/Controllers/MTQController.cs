using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NienLuanCoSo.Areas.Admin.Controllers
{
    public class MTQController : Controller
    {
        NIENLUANCOSOEntities4 db = new NIENLUANCOSOEntities4();
        // GET: Admin/MTQ
        public ActionResult Index(string Search = "")
        {
            if (Search == "top10")
            {
                var top =
                (from p in db.MANHTHUONGQUANs
                 let totalQuantity = (from op in db.TT_QUYENGOP_HIENVAT

                                      where op.MA_MTQ == p.MA_MTQ
                                      select op.MA_CD).Sum()
                 where totalQuantity > 0
                 orderby totalQuantity descending
                 select p).Take(10).ToList();
                return View(top);
            }
            if (Search == "all")
            {

                var listMTQ = from s in db.MANHTHUONGQUANs select s;
                return View(listMTQ);
            }
            
                var listMTQ1 = from s in db.MANHTHUONGQUANs select s;
                return View(listMTQ1);
            

        }

        // GET: Admin/MTQ/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/MTQ/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/MTQ/Create
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

        // GET: Admin/MTQ/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/MTQ/Edit/5
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

        // GET: Admin/MTQ/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/MTQ/Delete/5
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
