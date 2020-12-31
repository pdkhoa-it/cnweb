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
using Excel = Microsoft.Office.Interop.Excel;

namespace NoiThat_v1._0.Controllers
{
    public class DonHangController : Controller
    {
        // GET: DonHang
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DonHang()
        {
            return View();
        }

        public ActionResult ChiTietDH()
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                var list = db.SanPhams.ToList();
                SelectList listSP = new SelectList(list, "ID", "Ten");
                ViewBag.listSP = listSP;
            }
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
                                                    ThoiGian = d.ThoiGian,
                                                    DiaChiGiaoHang = d.DiaChiGiaoHang,
                                                    TinhTrangThanhToan = d.TinhTrangThanhToan,
                                                    TinhTrangGiaoHang = d.TinhTrangGiaoHang,
                                                    TongTien = d.TongTien
                                               }).ToList();
                return Json(new { data = list }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetDanhSachCTDH(int id)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                List<ChiTietDHViewModel> list = (from ct in db.ChiTietDonHangs
                                                 join dh in db.DonHangs on ct.IDDonHang equals dh.ID
                                                 join sp in db.SanPhams on ct.IDSanPham equals sp.ID
                                                 join dm in db.DanhMucSanPhams on sp.IDDanhMucSP equals dm.ID
                                                 join nh in db.NhomSanPhams on dm.IDNhomSP equals nh.ID
                                                 join ncc in db.NhaCungCaps on sp.IDNCC equals ncc.ID
                                                 where (ct.IDDonHang == id)
                                                 select new ChiTietDHViewModel
                                                 {
                                                     IDCT = ct.IDCT,
                                                     IDSanPham = sp.ID,
                                                     TenSanPham = sp.Ten,
                                                     TenDanhMuc = dm.Ten,
                                                     TenNhom = nh.Ten,
                                                     TenNhaCungCap = ncc.Ten,
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
                DonHangViewModel dh = (from d in db.DonHangs.Where(p => p.ID == id)
                                         select new DonHangViewModel
                                         {
                                             ID = d.ID,
                                             HoTen = d.HoTen,
                                             SDT = d.Sdt,
                                             Email = d.Email,
                                             HinhThucThanhToan = d.HinhThucThanhToan,
                                             ThoiGian = d.ThoiGian,
                                             DiaChiGiaoHang = d.DiaChiGiaoHang,
                                             TinhTrangThanhToan = d.TinhTrangThanhToan,
                                             TinhTrangGiaoHang = d.TinhTrangGiaoHang,
                                             TongTien = d.TongTien
                                             
                                         }).FirstOrDefault();
                return Json(dh, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult BeginEditCTDH(int id)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                ChiTietDHViewModel ct = (from ctdh in db.ChiTietDonHangs.Where(p => p.IDCT == id)
                                       select new ChiTietDHViewModel
                                       {
                                           IDCT = ctdh.IDCT,
                                           IDSanPham = ctdh.IDSanPham,
                                           SoLuong = ctdh.SoLuong,
                                           DonGia = ctdh.DonGia
                                       }).FirstOrDefault();
                return Json(ct, JsonRequestBehavior.AllowGet);
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
                if (!Directory.Exists(Server.MapPath("~/storage/Don-hang/" + d.ThoiGian)))
                    Directory.CreateDirectory(Server.MapPath("~/storage/Don-hang/" + d.ThoiGian));


                if (d.ID == 0)
                {
                    d.TinhTrangThanhToan = 0;
                    d.TinhTrangGiaoHang = 0;
                    d.TongTien = 0;
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
        public ActionResult AddOrEditCTDH(ChiTietDonHang ct)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                if (ct.IDCT == 0)
                {
                    if (db.ChiTietDonHangs.Where(p => p.IDSanPham == ct.IDSanPham).FirstOrDefault() != null)
                        return Json(new { success = false, message = "Sản phẩm đã tồn tại trong đơn hàng!" }, JsonRequestBehavior.AllowGet);

                    ct.ThanhTien = ct.SoLuong * ct.DonGia;
                    db.ChiTietDonHangs.Add(ct);

                    DonHang dh = db.DonHangs.Where(p => p.ID == ct.IDDonHang).FirstOrDefault();
                    dh.TongTien = dh.TongTien + ct.ThanhTien;

                    db.SaveChanges();
                    return Json(new { success = true, message = "Thêm mới thành công!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    try
                    {
                        ChiTietDonHang ct2 = db.ChiTietDonHangs.Where(p => p.IDCT == ct.IDCT).FirstOrDefault();
                        DonHang dh = db.DonHangs.Where(p => p.ID == ct.IDDonHang).FirstOrDefault();

                        dh.TongTien = dh.TongTien - ct2.ThanhTien;

                        ct2.IDSanPham = ct.IDSanPham;
                        ct2.SoLuong = ct.SoLuong;
                        ct2.DonGia = ct.DonGia;
                        ct2.ThanhTien = ct2.SoLuong * ct2.DonGia;

                        dh.TongTien = dh.TongTien + ct2.ThanhTien;

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

        [HttpPost]
        public ActionResult DeleteCTDH(int id)
        {
            try
            {
                using (DBNoiThat db = new DBNoiThat())
                {
                    ChiTietDonHang ct = db.ChiTietDonHangs.Where(p => p.IDCT == id).FirstOrDefault();
                    DonHang dh = db.DonHangs.Where(p => p.ID == ct.IDDonHang).FirstOrDefault();
                    dh.TongTien = dh.TongTien - ct.ThanhTien;
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

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase excelfile)
        {
            if (excelfile == null || excelfile.ContentLength == 0)
            {
                return Json(new { success = false, message = "Chưa chọn file hoặc file rỗng!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string path = Server.MapPath("~/storage/" + excelfile.FileName);

                excelfile.SaveAs(path);

                Excel.Application application = new Excel.Application();
                Excel.Workbook workbook = application.Workbooks.Open(path);
                Excel.Worksheet worksheet = workbook.ActiveSheet;
                Excel.Range range = worksheet.UsedRange;

                using (DBNoiThat db = new DBNoiThat())
                {
                    for (int row = 3; row <= range.Rows.Count; row++)
                    {
                        DonHang dh = new DonHang();
                        dh.HoTen = ((Excel.Range)range.Cells[row, 1]).Text;
                        dh.Sdt = ((Excel.Range)range.Cells[row, 2]).Text;
                        dh.Email = ((Excel.Range)range.Cells[row, 3]).Text;
                        dh.ThoiGian = ((Excel.Range)range.Cells[row, 4]).Text;
                        dh.DiaChiGiaoHang = ((Excel.Range)range.Cells[row, 5]).Text;
                        if (((Excel.Range)range.Cells[row, 6]).Text == "Thanh toán khi nhận hàng")
                            dh.HinhThucThanhToan = 0;
                        else dh.HinhThucThanhToan = 1;

                        if (((Excel.Range)range.Cells[row, 7]).Text == "Chờ thanh toán")
                            dh.TinhTrangThanhToan = 0;
                        else dh.TinhTrangThanhToan = 1;

                        if (((Excel.Range)range.Cells[row, 8]).Text == "Đang giao hàng")
                            dh.TinhTrangGiaoHang = 0;
                        else dh.TinhTrangGiaoHang = 1;

                        dh.TongTien = 0;

                        db.DonHangs.Add(dh);
                    }
                    db.SaveChanges();
                }
                workbook.Close();
                application.Quit();

                System.IO.File.Delete(path);

                return Json(new { success = true, message = "Nhập thành công~" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ImportCTDH(int IDDonHangImport, HttpPostedFileBase excelfile)
        {
            if (excelfile == null || excelfile.ContentLength == 0)
            {
                return Json(new { success = false, message = "Chưa chọn file hoặc file rỗng!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string path = Server.MapPath("~/storage/" + excelfile.FileName);

                excelfile.SaveAs(path);

                Excel.Application application = new Excel.Application();
                Excel.Workbook workbook = application.Workbooks.Open(path);
                Excel.Worksheet worksheet = workbook.ActiveSheet;
                Excel.Range range = worksheet.UsedRange;

                using (DBNoiThat db = new DBNoiThat())
                {
                    for (int row = 3; row <= range.Rows.Count; row++)
                    {
                        ChiTietDonHang ct = new ChiTietDonHang();
                        ct.IDDonHang = IDDonHangImport;
                        ct.IDSanPham = GetIDSanPham(((Excel.Range)range.Cells[row, 1]).Text);
                        ct.SoLuong = decimal.Parse(((Excel.Range)range.Cells[row, 5]).Text);
                        ct.DonGia = decimal.Parse(((Excel.Range)range.Cells[row, 6]).Text);
                        ct.ThanhTien = decimal.Parse(((Excel.Range)range.Cells[row, 7]).Text);

                        DonHang dh = db.DonHangs.Where(p => p.ID == IDDonHangImport).FirstOrDefault();
                        dh.TongTien = dh.TongTien + ct.ThanhTien;

                        db.ChiTietDonHangs.Add(ct);
                    }
                    db.SaveChanges();
                }
                workbook.Close();
                application.Quit();

                System.IO.File.Delete(path);

                return Json(new { success = true, message = "Nhập thành công!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public int GetIDSanPham(string ten)
        {
            using(DBNoiThat db = new DBNoiThat())
            {
                return db.SanPhams.Where(p => p.Ten == ten).FirstOrDefault().ID;
            }    
        }
    }
}