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
    public class ChiTietDHController : Controller
    {
        // GET: DonHang
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetDanhSach(int id)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                List<ChiTietDHViewModel> list = (from ct in db.ChiTietDonHangs join dh in db.DonHangs on ct.IDDonHang equals dh.ID
                                                 join sp in db.SanPhams on ct.IDSanPham equals sp.ID
                                                 join dm in db.DanhMucSanPhams on sp.IDDanhMucSP equals dm.ID
                                                 join nh in db.NhomSanPhams on dm.IDNhomSP equals nh.ID
                                                 join ncc in db.NhaCungCaps on sp.IDNCC equals ncc.ID
                                                 where(ct.ID == id)
                                               select new ChiTietDHViewModel
                                               {
                                                   ID = ct.ID,
                                                   IDSanPham = sp.ID,
                                                   TenSanPham = sp.Ten,
                                                   TenDanhMuc = dm.Ten,
                                                   TenNhom = nh.Ten,
                                                   TenNhaCungCap = ncc.Ten,
                                                   IDDonHang = ct.IDDonHang,
                                                   SoLuong = ct.SoLuong,
                                                   DonGia = ct.DonGia,
                                                   ThanhTien = ct.ThanhTien
                                               }).ToList();
                return Json(new { data = list }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult BeginEdit(int id)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                ChiTietDHViewModel ctdh = (from ct in db.ChiTietDonHangs
                                         join dh in db.DonHangs on ct.IDDonHang equals dh.ID
                                         join sp in db.SanPhams on ct.IDSanPham equals sp.ID
                                         join dm in db.DanhMucSanPhams on sp.IDDanhMucSP equals dm.ID
                                         join nh in db.NhomSanPhams on dm.IDNhomSP equals nh.ID
                                         join ncc in db.NhaCungCaps on sp.IDNCC equals ncc.ID
                                         where (ct.ID == id)
                                         select new ChiTietDHViewModel
                                         {
                                             ID = ct.ID,
                                             IDSanPham = sp.ID,
                                             SoLuong = ct.SoLuong,
                                             DonGia = ct.DonGia,
                                             ThanhTien = ct.ThanhTien
                                         }).FirstOrDefault();
                return Json(ctdh, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(ChiTietDonHang ct)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                if (ct.ID == 0)
                {
                    db.ChiTietDonHangs.Add(ct);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Thêm mới thành công!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    try
                    {
                        db.Entry(ct).State = EntityState.Modified;
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
                    ChiTietDonHang ct = db.ChiTietDonHangs.Where(p => p.ID == id).FirstOrDefault();
                    db.ChiTietDonHangs.Remove(ct);
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