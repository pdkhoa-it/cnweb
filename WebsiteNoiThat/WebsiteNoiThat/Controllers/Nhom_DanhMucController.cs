using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteNoiThat.Models;
using WebsiteNoiThat.ViewModels;

namespace WebsiteNoiThat.Controllers
{
    public class Nhom_DanhMucController : Controller
    {
        // GET: Nhom_DanhMuc
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewNhom()
        {
            return View(GetAllNhomSP());
        }

        IEnumerable<NhomSanPham> GetAllNhomSP()
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                return db.NhomSanPhams.ToList<NhomSanPham>();
            }
        }

        public ActionResult ViewDanhMuc()
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                List<NhomSanPham> listNhom = db.NhomSanPhams.ToList();
                SelectList list = new SelectList(listNhom, "ID", "Ten");
                ViewBag.listNhom = list;
            }
            return View(GetAllDanhMuc());
        }

        IEnumerable<DanhMucSanPhamViewModel> GetAllDanhMuc()
        {
            List<DanhMucSanPhamViewModel> list = new List<DanhMucSanPhamViewModel>();
            using (DBNoiThat db = new DBNoiThat())
            {
                list = (from n in db.NhomSanPhams
                        join d in db.DanhMucSanPhams on n.ID equals d.IDNhomSP
                        select new DanhMucSanPhamViewModel
                        {
                            ID = d.ID,
                            Ten = d.Ten,
                            NhomSP_ID = d.IDNhomSP,
                            TenNhomSP = n.Ten
                        }).ToList();
            }
            return list;
        }

        [HttpPost]
        public string AddOrEdit_Nhom(NhomSanPham n)
        {
            if (n.ID == 0)
            {
                using (DBNoiThat db = new DBNoiThat())
                {
                    db.NhomSanPhams.Add(n);
                    db.SaveChanges();
                }
                return "Thêm thành công!";
            }
            else
            {
                using (DBNoiThat db = new DBNoiThat())
                {
                    NhomSanPham n2 = db.NhomSanPhams.Where(p => p.ID == n.ID).FirstOrDefault();
                    n2.Ten = n.Ten;
                    db.SaveChanges();
                }
                return "Sửa thành công!";
            }
        }

        [HttpPost]
        public string Delete_Nhom(int id)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                NhomSanPham n = db.NhomSanPhams.Where(p => p.ID == id).FirstOrDefault();
                db.NhomSanPhams.Remove(n);
                db.SaveChanges();
            }
            return "Xóa thành công!";
        }

        [HttpPost]
        public string AddOrEdit_DM(DanhMucSanPham d)
        {
            if (d.ID == 0)
            {
                using (DBNoiThat db = new DBNoiThat())
                {
                    db.DanhMucSanPhams.Add(d);
                    db.SaveChanges();
                }
                return "Thêm thành công!";
            }
            else
            {
                using (DBNoiThat db = new DBNoiThat())
                {
                    DanhMucSanPham d2 = db.DanhMucSanPhams.Where(p => p.ID == d.ID).FirstOrDefault();
                    d2.Ten = d.Ten;
                    d2.IDNhomSP = d.IDNhomSP;
                    db.SaveChanges();
                }
                return "Sửa thành công!";
            }
        }

        [HttpPost]
        public string Delete_DM(int id)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                DanhMucSanPham d = db.DanhMucSanPhams.Where(p => p.ID == id).FirstOrDefault();
                db.DanhMucSanPhams.Remove(d);
                db.SaveChanges();
            }
            return "Xóa thành công!";
        }
    }
}