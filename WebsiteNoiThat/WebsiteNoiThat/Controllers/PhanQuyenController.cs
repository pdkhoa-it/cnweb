using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteNoiThat.Models;

namespace WebsiteNoiThat.Controllers
{
    public class PhanQuyenController : Controller
    {
        // GET: PhanQuyen
        public ActionResult Index()
        {
            ViewBag.m = "Thêm thành công!";
            return View(GetAllPhanQuyen());
        }

        IEnumerable<PhanQuyen> GetAllPhanQuyen()
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                return db.PhanQuyens.ToList<PhanQuyen>();
            }
        }

        [HttpPost]
        public JsonResult AddOrEdit(PhanQuyen p)
        {
            if (p.Ma == 0)
            {
                try
                {
                    using (DBNoiThat db = new DBNoiThat())
                    {
                        db.PhanQuyens.Add(p);
                        db.SaveChanges();
                    }
                    return Json(new { kq = true, message = "Thêm thành công!" }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(new { kq = false, message = "Không thể thêm dữ liệu!" }, JsonRequestBehavior.AllowGet);
                }
                
            }
            else
            {
                try
                {
                    using (DBNoiThat db = new DBNoiThat())
                    {
                        PhanQuyen pp = db.PhanQuyens.Where(c => c.Ma == p.Ma).FirstOrDefault();
                        pp.Ten = p.Ten;
                        db.SaveChanges();
                    }
                    return Json(new { kq = true, message = "Sửa thành công!" }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(new { kq = false, message = "Bạn không thể sửa trường dữ liệu này!" }, JsonRequestBehavior.AllowGet);
                }
                
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                using (DBNoiThat db = new DBNoiThat())
                {
                    PhanQuyen p = db.PhanQuyens.Where(c => c.Ma == id).FirstOrDefault();
                    db.PhanQuyens.Remove(p);
                    db.SaveChanges();
                }


                return Json(new {kq = true, message = "Xóa thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch 
            {
                return Json(new {kq = false, message = "Bạn không thể xóa trường dữ liệu này!" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}