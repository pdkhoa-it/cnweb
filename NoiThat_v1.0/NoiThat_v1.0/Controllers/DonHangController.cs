using NoiThat_v1._0.Models;
using NoiThat_v1._0.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoiThat_v1._0.Controllers
{
    public class DonHangController : Controller
    {
        // GET: DonHang
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetDanhSach()
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                List<DonHangViewModel> list = (from d in db.DonHangs
                                               select new DonHangViewModel { 
                                                    ID = d.ID,
                                                    HoTen = d.HoTen,
                                                    SDT = d.Sdt,
                                                    Email = d.Email,
                                                    HinhThucThanhToan = d.HinhThucThanhToan,
                                                    NgayThang = d.NgayThang,
                                                    DiaChiGiaoHang = d.DiaChiGiaoHang,
                                                    TinhTrangThanhToan = d.TinhTrangThanhToan,
                                                    TinhTrangGiaoHang = d.TinhTrangGiaoHang,
                                                    TongTien = d.TongTien
                                               }).ToList();
                return Json(new { data = list }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult BeginEdit(int id)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                DonHangViewModel dh = (from d in db.DonHangs.Where(p => p.ID == id)
                                         select new DonHangViewModel
                                         {
                                             ID = d.ID,
                                             HoTen = d.HoTen,
                                             SDT = d.Sdt,
                                             Email = d.Email,
                                             HinhThucThanhToan = d.HinhThucThanhToan,
                                             NgayThang = d.NgayThang,
                                             DiaChiGiaoHang = d.DiaChiGiaoHang,
                                             TinhTrangThanhToan = d.TinhTrangThanhToan,
                                             TinhTrangGiaoHang = d.TinhTrangGiaoHang,
                                             TongTien = d.TongTien
                                             
                                         }).FirstOrDefault();
                return Json(dh, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(DonHang d)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                //Kiểm tra và tạo thư mục đơn hàng
                if (!Directory.Exists(Server.MapPath("~/storage/Don-hang")))
                    Directory.CreateDirectory(Server.MapPath("~/storage/Don-hang"));

                //Kiểm tra và tạo thư mục cho ngày
                if (!Directory.Exists(Server.MapPath("~/storage/Don-hang/" + d.NgayThang)))
                    Directory.CreateDirectory(Server.MapPath("~/storage/Don-hang/" + d.NgayThang));


                if (d.ID == 0)
                {
                    db.DonHangs.Add(d);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Thêm mới thành công!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    try
                    {
                        db.Entry(d).State = EntityState.Modified;
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
                    DonHang d = db.DonHangs.Where(p => p.ID == id).FirstOrDefault();
                    db.DonHangs.Remove(d);
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