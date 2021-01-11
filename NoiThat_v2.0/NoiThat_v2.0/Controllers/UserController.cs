using NoiThat_v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NoiThat_v2._0.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult User()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(TaiKhoan tk)
        {
            using(DBNoiThat db = new DBNoiThat())
            {
                TaiKhoan tk2 = db.TaiKhoans.Where(p => p.ID == tk.ID).FirstOrDefault();

                int check = db.TaiKhoans.Where(p => p.Email == tk.Email).FirstOrDefault().ID;

                if(check != tk2.ID && check!=0)
                {
                    return Json(new { success = false, message = "Email đã được đăng ký cho một tài khoản khác!" }, JsonRequestBehavior.AllowGet);
                }

                tk2.HoTen = tk.HoTen;
                tk2.Sdt = tk.Sdt;
                tk2.Email = tk.Email;
                tk2.DiaChi = tk.DiaChi;
                tk2.GioiTinh = tk.GioiTinh;
                tk2.NgaySinh = tk.NgaySinh;

                Session.Remove("user");
                Session.Add("user", tk2);

                db.SaveChanges();

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ChangePass(TaiKhoan tk, string MatKhauCu)
        {
            using(DBNoiThat db = new DBNoiThat())
            {
                TaiKhoan tk2 = db.TaiKhoans.Where(p => p.ID == tk.ID).FirstOrDefault();

                if (tk2.MatKhau != GetMD5(MatKhauCu + tk2.Salt.ToString()))
                    return Json(new { success = false, message = "Sai mật khẩu cũ!" }, JsonRequestBehavior.AllowGet);

                tk2.MatKhau = GetMD5(tk.MatKhau + tk2.Salt.ToString());
                tk2.XacNhanMatKhau = tk2.MatKhau;

                db.SaveChanges();
            }    
            return Json(new { success = true, message = "Đổi mật khẩu thành công!" }, JsonRequestBehavior.AllowGet);
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