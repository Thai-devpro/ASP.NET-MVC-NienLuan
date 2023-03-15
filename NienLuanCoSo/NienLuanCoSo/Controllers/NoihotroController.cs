using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NienLuanCoSo.Controllers
{
    public class NoihotroController : Controller
    {
        // GET: Noihotro
        NIENLUANCOSOEntities4 db = new NIENLUANCOSOEntities4();
        public ActionResult Index()
        {
            if (Session["Taikhoanmtq"] == null)
            {
                Session["noihotro"] = "có";
                return RedirectToAction("Create", "User");
               
            }
            int id = Convert.ToInt32(Session["idmtq"]);
            //NOIHOTRO nht = db.NOIHOTROes.Find(id);
            var nht = db.NOIHOTROes.Where(p => p.MA__MTQ.Equals(id));


            return View(nht);
            
        }

        // GET: Noihotro/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Noihotro/Create
        public ActionResult Create()
        {
            return View();
        }
        
        // POST: Noihotro/Create
        [HttpPost, ActionName("Create")]
        [ValidateInput(false)]
        public ActionResult CreateBook(NOIHOTRO nht, HttpPostedFileBase image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(nht.CANHOTRO) == true)
                    {
                        ModelState.AddModelError("", "Bạn cần hỗ trợ gì?");
                        return View(nht);
                    }
                    if (string.IsNullOrEmpty(nht.DIACHI) == true)
                    {
                        ModelState.AddModelError("", "Vui lòng nhập địa chỉ!");
                        return View(nht);
                    }
                    if (string.IsNullOrEmpty(nht.TINHTRANG) == true)
                    {
                        ModelState.AddModelError("", "Vui lòng nhập tình trạng!");
                        return View(nht);
                    }
                    if (image != null && image.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(image.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/images/noihotro/"), _FileName);
                        image.SaveAs(_path);
                        nht.ANH_NTH = image.FileName;
                    }
                    if (string.IsNullOrEmpty(nht.ANH_NTH) == true)
                    {
                        ModelState.AddModelError("", "Vui lòng chọn ảnh làm minh chứng!");
                        return View(nht);
                    }


                    nht.MA__MTQ = Convert.ToInt32(Session["idmtq"]);
                    nht.DIACHI = nht.DIACHI.Trim();
                    nht.CANHOTRO = nht.CANHOTRO.Trim();
                    nht.TINHTRANG = nht.TINHTRANG.Trim();
                    nht.TRANGTHAI_NHT = "Chưa duyệt";
                    db.NOIHOTROes.Add(nht);
                    db.SaveChanges();
                    
                    
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Error Save Data");
            }
            //Cập nhật lại danh sách hiển thị
            var listnth = from s in db.NOIHOTROes select s;
            return RedirectToAction("index", "Noihotro");
        }


        // GET: Noihotro/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NOIHOTRO nht = db.NOIHOTROes.SingleOrDefault(s => s.MANOI == id);
            return View(nht);

        }

        // POST: Noihotro/Edit/5
        [HttpPost]
        public ActionResult Edit(NOIHOTRO nht, HttpPostedFileBase image)
        {
            
            
                try
                {
                    if (image != null && image.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(image.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/images/noihotro/"), _FileName);
                        image.SaveAs(_path);
                        nht.ANH_NTH = image.FileName;
                    }
                NOIHOTRO nhtu = db.NOIHOTROes.SingleOrDefault(s => s.MANOI == nht.MANOI);
                if (string.IsNullOrEmpty(nht.CANHOTRO) == true)
                    {
                        ModelState.AddModelError("", "Bạn cần hỗ trợ gì?");
                        return View(nht);
                    }
                    if (string.IsNullOrEmpty(nht.DIACHI) == true)
                    {
                        ModelState.AddModelError("", "Vui lòng nhập địa chỉ!");
                        return View(nht);
                    }
                    if (string.IsNullOrEmpty(nht.TINHTRANG) == true)
                    {
                        ModelState.AddModelError("", "Vui lòng nhập tình trạng!");
                        return View(nht);
                }

                if (nht.ANH_NTH != null)
                    nhtu.ANH_NTH = nht.ANH_NTH;
                if(nht.ANH_NTH == null)
                {
                    nhtu.ANH_NTH = nhtu.ANH_NTH;
                }

                    
                    nhtu.MA__MTQ = Convert.ToInt32(Session["idmtq"]);
                    nhtu.DIACHI = nht.DIACHI.Trim();
                    nhtu.CANHOTRO = nht.CANHOTRO.Trim();
                    nhtu.TINHTRANG = nht.TINHTRANG.Trim();
                    db.Entry(nhtu).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Error Save Data");
                }
                return RedirectToAction("Index");
            

        }

        // GET: Noihotro/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NOIHOTRO nht = db.NOIHOTROes.SingleOrDefault(s => s.MANOI == id);
            return View(nht);
        }

        // POST: Noihotro/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult Deleteconfirm(int? id )
        {
            NOIHOTRO nht = db.NOIHOTROes.SingleOrDefault(n => n.MANOI == id);
            var path = Path.Combine(Server.MapPath("~/Content/images/noihotro"),nht.ANH_NTH);

            if (nht == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Xoá ảnh trong thư mục ~/Content/Image
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            db.NOIHOTROes.Remove(nht);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
