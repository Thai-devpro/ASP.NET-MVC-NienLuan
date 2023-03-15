using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NienLuanCoSo.Controllers
{
    public class ChienDichController : Controller
    {
        NIENLUANCOSOEntities4 db = new NIENLUANCOSOEntities4();
        // GET: ChienDich
        public ActionResult Index(string SearchString="")
        {
            if(SearchString != "")
            {
                var ChienDich = db.CHIENDICHes.Where(s => s.TEN_CD.ToUpper().Contains(SearchString.ToUpper()));
                return View(ChienDich.ToList());
            }
            var listChienDich = from s in db.CHIENDICHes select s;
            return View(listChienDich);
        }

        // GET: ChienDich/Details/5
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          CHIENDICH movie = db.CHIENDICHes.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: ChienDich/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChienDich/Create
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

        // GET: ChienDich/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ChienDich/Edit/5
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

        // GET: ChienDich/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ChienDich/Delete/5
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
