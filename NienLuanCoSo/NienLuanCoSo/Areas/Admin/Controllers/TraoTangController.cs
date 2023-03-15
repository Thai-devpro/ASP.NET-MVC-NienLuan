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
    public class TraoTangController : Controller
    {
        NIENLUANCOSOEntities4 db = new NIENLUANCOSOEntities4();
        // GET: Admin/TraoTang
        public ActionResult Index()
        {

            var listTraotang = from s in db.TT_TRAOTANG select s;
            return View(listTraotang);
        }

        // GET: Admin/TraoTang/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/TraoTang/Create
        public ActionResult Create()
        {
            var nht = from s in db.NOIHOTROes where s.TRANGTHAI_NHT=="Đã duyệt" select s;
            //var nht = db.NOIHOTROes.Where(s => s.TRANGTHAI_NHT.Trim().Equals("Đã duyệt")).ToList();
            ViewBag.MANOI = new SelectList(nht, dataValueField: "MANOI", dataTextField: "DIACHI");
            var hv = db.HIEN_VAT.ToList();
            ViewBag.MA_HV = new SelectList(hv, dataValueField: "MA_HV", dataTextField: "TEN_HV");
            var chiendichlist = db.CHIENDICHes.ToList();
            ViewBag.MA_CD = new SelectList(chiendichlist, dataValueField: "MA_CD", dataTextField: "TEN_CD");
            return View();
        }
        // POST: Admin/TraoTang/Create
        [HttpPost, ActionName("Create")]
        public ActionResult Create(TT_TRAOTANG tt, HttpPostedFileBase image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (image != null && image.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(image.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/images/traotang/"), _FileName);
                        image.SaveAs(_path);
                        if (System.IO.File.Exists(_path))
                        {
                            ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                        }
                        else
                        {
                            image.SaveAs(_path);
                        }
                        //Them Sach Moi
                    }

                    tt.ANH_TT = image.FileName;
                    if (tt.SOLUONG_TT <= 0 || tt.SOLUONG_TT == null)
                    {
                        ModelState.AddModelError("", "Vui lòng nhập số lượng!");
                        return View(tt);
                    }
                    if (image == null)
                    {
                        ModelState.AddModelError("", "Vui lòng thêm ảnh trao tặng!");
                        return View(tt);
                    }

                    db.TT_TRAOTANG.Add(tt);
                    db.SaveChanges();

                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Error Save Data");
            }
            //Cập nhật lại danh sách hiển thị
            var listnth = from s in db.NOIHOTROes select s;
            return RedirectToAction("index", "Traotang");
        }

        // GET: Admin/TraoTang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nht = db.NOIHOTROes.ToList();
            ViewBag.MANOI = new SelectList(nht, dataValueField: "MANOI", dataTextField: "DIACHI");
            var hv = db.HIEN_VAT.ToList();
            ViewBag.MA_HV = new SelectList(hv, dataValueField: "MA_HV", dataTextField: "TEN_HV");
            var chiendichlist = db.CHIENDICHes.ToList();
            ViewBag.MA_CD = new SelectList(chiendichlist, dataValueField: "MA_CD", dataTextField: "TEN_CD");
           
            TT_TRAOTANG tt = db.TT_TRAOTANG.SingleOrDefault(s => s.MA_TT == id);
            return View(tt);

        }

        // POST: Noihotro/Edit/5
        [HttpPost]
        public ActionResult Edit(TT_TRAOTANG tt, HttpPostedFileBase image)
        {

            
            TT_TRAOTANG ttu = db.TT_TRAOTANG.SingleOrDefault(s => s.MA_TT == tt.MA_TT);
            try
            {

                
                if (tt.SOLUONG_TT <= 0 || tt.SOLUONG_TT == null)
                {
                    ModelState.AddModelError("", "Vui lòng nhập số lượng!");
                    return View(tt);
                }
                
                if (image != null && image.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(image.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/images/traotang/"), _FileName);
                    image.SaveAs(_path);
                    if (System.IO.File.Exists(_path))
                    {
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        image.SaveAs(_path);
                    }
                    tt.ANH_TT = image.FileName;
                }
                if (tt.ANH_TT != null)
                    ttu.ANH_TT = tt.ANH_TT;
                if (tt.ANH_TT == null)
                    ttu.ANH_TT = ttu.ANH_TT;

                ttu.MANOI = tt.MANOI;
                ttu.MA_HV = tt.MA_HV;
                ttu.MA_CD = tt.MA_CD;

                ttu.SOLUONG_TT = tt.SOLUONG_TT;
                ttu.NGAYTANG = tt.NGAYTANG;
                db.Entry(ttu).State = EntityState.Modified;
                db.SaveChanges();

            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Error Save Data");
            }


            return RedirectToAction("index","TraoTang");

        }

        // GET: Admin/TraoTang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TT_TRAOTANG nht = db.TT_TRAOTANG.SingleOrDefault(s => s.MA_TT == id);
            return View(nht);
        }

        // POST: Noihotro/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult Deleteconfirm(int? id)
        {
            TT_TRAOTANG nht = db.TT_TRAOTANG.SingleOrDefault(n => n.MA_TT == id);
            var path = Path.Combine(Server.MapPath("~/Content/images/traotang/"), nht.ANH_TT);

            if (nht == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Xoá ảnh trong thư mục ~/Content/Image
            //if (System.IO.File.Exists(path))
            //{
            //    System.IO.File.Delete(path);
            //}

            db.TT_TRAOTANG.Remove(nht);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
