using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NienLuanCoSo.Areas.Admin.Controllers
{
    public class TK_MTQController : Controller
    {
        NIENLUANCOSOEntities4 db = new NIENLUANCOSOEntities4();
        // GET: Admin/TK_MTQ
        public ActionResult Index()
        {
            var listTKMTQ = from s in db.TAIKHOAN_MTQ select s;
            return View(listTKMTQ);
        }

        // GET: Admin/TK_MTQ/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/TK_MTQ/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TK_MTQ/Create
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

        // GET: Admin/TK_MTQ/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/TK_MTQ/Edit/5
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

        // GET: Admin/TK_MTQ/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/TK_MTQ/Delete/5
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
