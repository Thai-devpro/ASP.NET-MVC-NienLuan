using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NienLuanCoSo.Controllers
{
    public class TraotangController : Controller
    {
        NIENLUANCOSOEntities4 db = new NIENLUANCOSOEntities4();
        // GET: Traotang
        public ActionResult Index()
        {
            var listTraotang = from s in db.TT_TRAOTANG select s;
            return View(listTraotang);
        }

        // GET: Traotang/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Traotang/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Traotang/Create
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

        // GET: Traotang/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Traotang/Edit/5
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

        // GET: Traotang/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Traotang/Delete/5
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
