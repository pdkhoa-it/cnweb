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
        public string AddOrEdit(PhanQuyen p)
        {
            if (p.Ma == 0)
            {
                using (DBNoiThat db = new DBNoiThat())
                {
                    db.PhanQuyens.Add(p);
                    db.SaveChanges();
                }
                return "Thêm thành công!";
            }
            else
            {
                using (DBNoiThat db = new DBNoiThat())
                {
                    PhanQuyen pp = db.PhanQuyens.Where(c => c.Ma == p.Ma).FirstOrDefault();
                    pp.Ten = p.Ten;
                    db.SaveChanges();
                }
                return "Sửa thành công!";
            }
        }

        [HttpPost]
        public string Delete(int id)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                PhanQuyen p = db.PhanQuyens.Where(c => c.Ma == id).FirstOrDefault();
                db.PhanQuyens.Remove(p);
                db.SaveChanges();
            }
            return "Xóa thành công!";
        }
    }
}