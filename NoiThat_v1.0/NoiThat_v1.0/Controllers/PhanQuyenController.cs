using NoiThat_v1._0.Models;
using NoiThat_v1._0.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoiThat_v1._0.Controllers
{
    public class PhanQuyenController : Controller
    {
        // GET: PhanQuyen
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetDanhSach()
        {
            using(DBNoiThat db = new DBNoiThat())
            {
                List<PhanQuyenViewModel> list = (from pq in db.PhanQuyens
                                                 select new PhanQuyenViewModel { 
                                                     ID = pq.ID,
                                                     Ten = pq.Ten
                                                 }).ToList();
                return Json(new { data = list }, JsonRequestBehavior.AllowGet);
            }    
        }

        [HttpGet]
        public ActionResult BeginEdit(int id)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                PhanQuyenViewModel pq = (from p in db.PhanQuyens.Where(c => c.ID == id)
                                         select new PhanQuyenViewModel { ID = p.ID, Ten = p.Ten }).FirstOrDefault();
                return Json(pq, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(PhanQuyen pq)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                if (pq.ID == 0)
                {
                    try
                    {
                        db.PhanQuyens.Add(pq);
                        db.SaveChanges();
                        return Json(new { success = true, message = "Thêm mới thành công!" }, JsonRequestBehavior.AllowGet);
                    }
                    catch
                    {
                        return Json(new { success = false, message = "Thêm mới thất bại!" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    try
                    {
                        db.Entry(pq).State = EntityState.Modified;
                        db.SaveChanges();
                        return Json(new { success = true, message = "Cập nhật thành công!" }, JsonRequestBehavior.AllowGet);
                    }
                    catch
                    {
                        return Json(new { success = false, message = "Cập nhật thất bại!" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                using (DBNoiThat db = new DBNoiThat())
                {
                    PhanQuyen pq = db.PhanQuyens.Where(p => p.ID == id).FirstOrDefault();
                    db.PhanQuyens.Remove(pq);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Xóa dữ liệu thành công!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { success = false, message = "Bạn không thể xóa trường này!" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}