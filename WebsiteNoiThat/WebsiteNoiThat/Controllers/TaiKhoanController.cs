using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebsiteNoiThat.Models;
using WebsiteNoiThat.ViewModels;

namespace WebsiteNoiThat.Controllers
{
    public class TaiKhoanController : Controller
    {
        // GET: TaiKhoan
        public ActionResult Index()
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                var listQuyen = db.PhanQuyens.ToList();
                SelectList list = new SelectList(listQuyen, "Ma", "Ten");
                ViewBag.listQuyen = list;
            }
            return View(GetAllTaiKhoan());
        }

        IEnumerable<TaiKhoanViewModel> GetAllTaiKhoan()
        {
            List<TaiKhoanViewModel> list = new List<TaiKhoanViewModel>();
            using (DBNoiThat db = new DBNoiThat())
            {
                list = (from tk in db.TaiKhoans
                        join pq in db.PhanQuyens on tk.MaQuyen equals pq.Ma
                        select new TaiKhoanViewModel
                        {
                            ID = tk.ID,
                            TenDangNhap = tk.TenDangNhap,
                            HoTen = tk.HoTen,
                            Email = tk.Email,
                            Sdt = tk.Sdt,
                            DiaChi = tk.DiaChi,
                            NgaySinh = tk.NgaySinh,
                            GioiTinh = tk.GioiTinh,
                            MaQuyen = tk.MaQuyen,
                            TenQuyen = pq.Ten
                        }).ToList();
            }
            return list;
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

        [HttpPost]
        public string AddOrEdit(TaiKhoan tk)
        {
            if (tk.ID == 0)
            {
                using (DBNoiThat db = new DBNoiThat())
                {
                    Random r = new Random();
                    int salt = r.Next(1, 1000);
                    tk.Salt = salt;
                    tk.MatKhau = GetMD5(tk.MatKhau + tk.Salt.ToString());
                    db.TaiKhoans.Add(tk);
                    db.SaveChanges();
                }
                return "Thêm thành công!";
            }
            else
            {
                using (DBNoiThat db = new DBNoiThat())
                {
                    TaiKhoan tk2 = db.TaiKhoans.Where(p => p.ID == tk.ID).FirstOrDefault();
                    tk2.HoTen = tk.HoTen;
                    tk2.GioiTinh = tk.GioiTinh;
                    tk2.Sdt = tk.Sdt;
                    tk2.NgaySinh = tk.NgaySinh;
                    tk2.Email = tk.Email;
                    tk2.DiaChi = tk.DiaChi;
                    tk2.MaQuyen = tk.MaQuyen;

                    db.SaveChanges();
                }
                return "Sửa thành công!";
            }
        }

        public string DeleteTaiKhoan(int id)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                TaiKhoan tk = db.TaiKhoans.Where(p => p.ID == id).FirstOrDefault();
                db.TaiKhoans.Remove(tk);
                db.SaveChanges();
            }
            return "Xóa thành công!";
        }

        [HttpPost]
        public string CheckPass(int id, string mk)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                TaiKhoan tk = db.TaiKhoans.Where(p => p.ID == id).FirstOrDefault();
                if (tk.MatKhau == GetMD5(mk + tk.Salt))
                {
                    return "OK";
                }
                else return "Not OK";
            }
        }

        [HttpPost]
        public string ChangePass(int id, string mk)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                TaiKhoan tk = db.TaiKhoans.Where(p => p.ID == id).FirstOrDefault();
                tk.MatKhau = GetMD5(mk + tk.Salt);
                db.SaveChanges();
            }
            return "Đổi mật khẩu thành công!";
        }
    }
}