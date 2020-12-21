using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteNoiThat.Models;

namespace WebsiteNoiThat.Controllers
{
    public class NhaCungCapController : Controller
    {
        // GET: NhaCungCap
        public ActionResult Index()
        {
            return View(GetAllNhaCungCap());
        }

        IEnumerable<NhaCungCap> GetAllNhaCungCap()
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                return db.NhaCungCaps.ToList<NhaCungCap>();
            }
        }

        [HttpPost]
        public string AddOrEdit(NhaCungCap n)
        {
            if (n.ID == 0)
            {
                using (DBNoiThat db = new DBNoiThat())
                {
                    db.NhaCungCaps.Add(n);
                    db.SaveChanges();
                }
                return "Thêm thành công!";
            }
            else
            {
                using (DBNoiThat db = new DBNoiThat())
                {
                    NhaCungCap n2 = db.NhaCungCaps.Where(p => p.ID == n.ID).FirstOrDefault();
                    n2.Ten = n.Ten;
                    n2.DiaChi = n.DiaChi;
                    n2.SoDienThoai = n.SoDienThoai;
                    n2.Email = n.Email;
                    n2.MaSoThue = n.MaSoThue;
                    n2.SoTaiKhoan = n.SoTaiKhoan;
                    n2.NguoiDaiDien = n.NguoiDaiDien;
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
                NhaCungCap n = db.NhaCungCaps.Where(p => p.ID == id).FirstOrDefault();
                db.NhaCungCaps.Remove(n);
                db.SaveChanges();
            }
            return "Xóa thành công!";
        }
    }
}