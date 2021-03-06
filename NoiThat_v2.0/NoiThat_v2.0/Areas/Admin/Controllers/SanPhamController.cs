﻿using NoiThat_v2._0.Areas.Admin.Models;
using NoiThat_v2._0.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;

namespace NoiThat_v2._0.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        public ActionResult Index()
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                var listDM = db.DanhMucSanPhams.ToList();
                SelectList list = new SelectList(listDM, "ID", "Ten");
                ViewBag.listDM = list;

                var listNCC = db.NhaCungCaps.ToList();
                list = new SelectList(listNCC, "ID", "Ten");
                ViewBag.listNCC = list;
            }
            return View();
        }

        [HttpGet]
        public ActionResult GetDanhSach()
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                List<SanPhamViewModel> list = (from s in db.SanPhams
                                               join n in db.NhaCungCaps on s.IDNCC equals n.ID
                                               join d in db.DanhMucSanPhams on s.IDDanhMucSP equals d.ID
                                               join nh in db.NhomSanPhams on d.IDNhomSP equals nh.ID
                                               select new SanPhamViewModel
                                               {
                                                   ID = s.ID,
                                                   Ten = s.Ten,
                                                   IDNCC = s.IDNCC,
                                                   TenNCC = n.Ten,
                                                   IDDM = s.IDDanhMucSP,
                                                   TenDM = d.Ten,
                                                   TenNhom = nh.Ten,
                                                   Mota = s.MoTa,
                                                   Gia = s.Gia,
                                                   Ten_img = s.Ten_img,
                                                   PathImg = "/storage/" + nh.Ten_slug + "/" + d.Ten_slug + "/" + s.Ten_img
                                               }).ToList();
                return Json(new { data = list }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult BeginEdit(int id)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                SanPhamViewModel sp = (from s in db.SanPhams
                                       join n in db.NhaCungCaps on s.IDNCC equals n.ID
                                       join d in db.DanhMucSanPhams on s.IDDanhMucSP equals d.ID
                                       join nh in db.NhomSanPhams on d.IDNhomSP equals nh.ID
                                       where (s.ID == id)
                                       select new SanPhamViewModel
                                       {
                                           ID = s.ID,
                                           Ten = s.Ten,
                                           IDNCC = s.IDNCC,
                                           IDDM = s.IDDanhMucSP,
                                           Gia = s.Gia,
                                           Mota = s.MoTa
                                       }).FirstOrDefault();
                return Json(sp, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(SanPham s)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                s.Ten_slug = RemoveUnicode(s.Ten);

                DanhMucSanPham d = db.DanhMucSanPhams.Where(p => p.ID == s.IDDanhMucSP).FirstOrDefault();
                NhomSanPham n = db.NhomSanPhams.Where(p => p.ID == d.IDNhomSP).FirstOrDefault();

                if (s.ID == 0)
                {
                    if (db.SanPhams.Where(p => p.Ten_slug == s.Ten_slug).FirstOrDefault() != null)
                    {
                        return Json(new { success = false, message = "Tên sản phẩm đã tồn tại!" }, JsonRequestBehavior.AllowGet);
                    }

                    if (s.ImgUpload != null)
                    {
                        //Lấy đuôi file ảnh
                        string ex = Path.GetExtension(s.ImgUpload.FileName);
                        //Lưu đường dẫn
                        s.Ten_img = s.Ten_slug + ex;
                        //Lưu file vào thư mục
                        s.ImgUpload.SaveAs(Path.Combine(Server.MapPath("~/storage/" + n.Ten_slug + "/" + d.Ten_slug + "/"), s.Ten_slug + ex));
                    }
                    else
                    {
                        //Copy và lưu file ảnh mặc định cho sản phẩm
                        System.IO.File.Copy(Server.MapPath("~/storage/default.png"), Server.MapPath("~/storage/" + n.Ten_slug + "/" + d.Ten_slug + "/" + s.Ten_slug + ".png"));
                        //Lưu đường dẫn
                        s.Ten_img = s.Ten_slug + ".png";
                    }

                    db.SanPhams.Add(s);
                    db.SaveChanges();

                    return Json(new { success = true, message = "Thêm mới thành công!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    SanPham ss = db.SanPhams.Where(p => p.ID == s.ID).FirstOrDefault();
                    SanPhamViewModel sp = (from sEdit in db.SanPhams
                                           join dEdit in db.DanhMucSanPhams on s.IDDanhMucSP equals d.ID
                                           join nh in db.NhomSanPhams on d.IDNhomSP equals nh.ID
                                           where (sEdit.ID == s.ID && sEdit.IDDanhMucSP == dEdit.ID && dEdit.IDNhomSP == nh.ID)
                                           select new SanPhamViewModel
                                           {
                                               PathImg = "~/storage/" + nh.Ten_slug + "/" + dEdit.Ten_slug + "/" + sEdit.Ten_img
                                           }).FirstOrDefault();

                    if (s.ImgUpload != null)
                    {
                        //Xóa ảnh cũ
                        System.IO.File.Delete(Server.MapPath(sp.PathImg));

                        //Lấy đuôi file ảnh
                        string ex = Path.GetExtension(s.ImgUpload.FileName);
                        //Lưu đường dẫn
                        s.Ten_img = s.Ten_slug + ex;
                        //Lưu file vào thư mục
                        s.ImgUpload.SaveAs(Path.Combine(Server.MapPath("~/storage/" + n.Ten_slug + "/" + d.Ten_slug + "/"), s.Ten_slug + ex));
                    }
                    else
                    {
                        //Lấy đuôi file ảnh
                        string ex = System.IO.Path.GetExtension(sp.PathImg);
                        if (!System.IO.File.Exists(Server.MapPath(sp.PathImg)))
                        {
                            //Kiểm tra file ảnh để lưu file
                            System.IO.File.Copy(Server.MapPath(sp.PathImg), Server.MapPath("~/storage/" + n.Ten_slug + "/" + d.Ten_slug + "/" + s.Ten_slug + ex));
                            //Lưu đường dẫn
                            s.Ten_img = s.Ten_slug + ex;
                            //Xóa ảnh cũ
                            System.IO.File.Delete(Server.MapPath(sp.PathImg));
                        }
                        s.Ten_img = s.Ten_slug + ex;
                    }

                    ss.Ten = s.Ten;
                    ss.Ten_slug = s.Ten_slug;
                    ss.Gia = s.Gia;
                    ss.MoTa = s.MoTa;
                    ss.IDDanhMucSP = s.IDDanhMucSP;
                    ss.IDNCC = ss.IDNCC;
                    ss.Ten_img = s.Ten_img;

                    db.SaveChanges();
                    return Json(new { success = true, message = "Cập nhật thành công!" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                try
                {
                    SanPhamViewModel sp = (from s in db.SanPhams
                                           join d in db.DanhMucSanPhams on s.IDDanhMucSP equals d.ID
                                           join nh in db.NhomSanPhams on d.IDNhomSP equals nh.ID
                                           where (s.ID == id)
                                           select new SanPhamViewModel
                                           {
                                               PathImg = "~/storage/" + nh.Ten_slug + "/" + d.Ten_slug + "/" + s.Ten_img
                                           }).FirstOrDefault();


                    //Xóa file ảnh
                    System.IO.File.Delete(Server.MapPath(sp.PathImg));

                    SanPham ss = db.SanPhams.Where(p => p.ID == id).FirstOrDefault();

                    db.SanPhams.Remove(ss);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Xóa dữ liệu thành công!" }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(new { success = false, message = "Xóa dữ liệu thất bại!" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public string RemoveUnicode(string text)
        {
            text = text.Trim();
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
                                                "đ",
                                                "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
                                                "í","ì","ỉ","ĩ","ị",
                                                "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
                                                "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
                                                "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                                                "d",
                                                "e","e","e","e","e","e","e","e","e","e","e",
                                                "i","i","i","i","i",
                                                "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
                                                "u","u","u","u","u","u","u","u","u","u","u",
                                                "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
                text = text.Replace(" ", "-");
            }
            return text;
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
                        SanPham s = new SanPham();

                        s.Ten = ((Excel.Range)range.Cells[row, 1]).Text;
                        s.Ten_slug = RemoveUnicode(s.Ten);
                        s.Gia = decimal.Parse(((Excel.Range)range.Cells[row, 2]).Text);
                        s.IDDanhMucSP = GetIDDanhMuc(((Excel.Range)range.Cells[row, 3]).Text);
                        s.IDNCC = GetIDNhaCungCap(((Excel.Range)range.Cells[row, 5]).Text);
                        s.Ten_img = ((Excel.Range)range.Cells[row, 6]).Text;
                        s.MoTa = ((Excel.Range)range.Cells[row, 7]).Text;

                        db.SanPhams.Add(s);
                    }
                    db.SaveChanges();
                }
                workbook.Close();
                application.Quit();

                System.IO.File.Delete(path);

                return Json(new { success = true, message = "OK" }, JsonRequestBehavior.AllowGet);
            }
        }

        public int GetIDNhaCungCap(string ten)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                return db.NhaCungCaps.Where(p => p.Ten == ten).FirstOrDefault().ID;
            }
        }

        public int GetIDDanhMuc(string ten)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                return db.DanhMucSanPhams.Where(p => p.Ten == ten).FirstOrDefault().ID;
            }
        }
    }
}