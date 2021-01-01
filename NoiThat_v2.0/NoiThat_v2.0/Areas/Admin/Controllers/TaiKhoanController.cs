using NoiThat_v2._0.Areas.Admin.Models;
using NoiThat_v2._0.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NoiThat_v2._0.Areas.Admin.Controllers
{
    public class TaiKhoanController : Controller
    {
        // GET: TaiKhoan
        public ActionResult Index()
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                var listQuyen = db.PhanQuyens.ToList();
                SelectList list = new SelectList(listQuyen, "ID", "Ten");
                ViewBag.listQuyen = list;
            }
            return View();
        }

        [HttpGet]
        public ActionResult GetDanhSach()
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                List<TaiKhoanViewModel> list = (from tk in db.TaiKhoans
                                                join pq in db.PhanQuyens on tk.IDQuyen equals pq.ID
                                                select new TaiKhoanViewModel
                                                {
                                                    ID = tk.ID,
                                                    HoTen = tk.HoTen,
                                                    Email = tk.Email,
                                                    Sdt = tk.Sdt,
                                                    DiaChi = tk.DiaChi,
                                                    NgaySinh = tk.NgaySinh,
                                                    GioiTinh = tk.GioiTinh,
                                                    IDQuyen = tk.IDQuyen,
                                                    TenQuyen = pq.Ten
                                                }).ToList();
                return Json(new { data = list }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult BeginEdit(int id)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                TaiKhoanViewModel tk = (from t in db.TaiKhoans.Where(p => p.ID == id)
                                        select new TaiKhoanViewModel
                                        {
                                            ID = t.ID,
                                            HoTen = t.HoTen,
                                            Email = t.Email,
                                            GioiTinh = t.GioiTinh,
                                            NgaySinh = t.NgaySinh,
                                            Sdt = t.Sdt,
                                            DiaChi = t.DiaChi,
                                            IDQuyen = t.IDQuyen,
                                            MatKhau = t.MatKhau
                                        }).FirstOrDefault();
                return Json(tk, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(TaiKhoan tk)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                if (tk.ID == 0)
                {
                    if (db.TaiKhoans.Where(p => p.Email == tk.Email).FirstOrDefault() != null)
                    {
                        return Json(new { success = false, message = "Email đã tồn tại! Hãy thử đăng ký với một email khác!" }, JsonRequestBehavior.AllowGet);
                    }
                    Random r = new Random();
                    tk.Salt = r.Next(100, 1000);
                    tk.MatKhau = GetMD5(tk.MatKhau + tk.Salt.ToString());
                    tk.XacNhanMatKhau = tk.MatKhau;
                    db.TaiKhoans.Add(tk);
                    db.SaveChanges();

                    return Json(new { success = true, message = "Thêm mới thành công!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    try
                    {
                        TaiKhoan tk2 = db.TaiKhoans.Where(p => p.ID == tk.ID).FirstOrDefault();
                        if (tk.MatKhau != tk2.MatKhau)
                        {
                            tk2.MatKhau = GetMD5(tk.MatKhau + tk.Salt.ToString());
                            tk2.XacNhanMatKhau = tk2.MatKhau;
                        }
                        tk2.HoTen = tk.HoTen;
                        tk2.Email = tk.Email;
                        tk2.NgaySinh = tk.NgaySinh;
                        tk2.Sdt = tk.Sdt;
                        tk2.GioiTinh = tk.GioiTinh;
                        tk2.GioiTinh = tk.GioiTinh;
                        tk2.IDQuyen = tk.IDQuyen;
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
                    TaiKhoan tk = db.TaiKhoans.Where(p => p.ID == id).FirstOrDefault();
                    db.TaiKhoans.Remove(tk);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Xóa dữ liệu thành công!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { success = false, message = "Bạn không thể xóa trường này!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public string GetMD5(string text)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder str = new StringBuilder();
            for (int i = 1; i < result.Length; i++)
            {
                str.Append(result[i].ToString("x2"));
            }
            return str.ToString();
        }
    }
}