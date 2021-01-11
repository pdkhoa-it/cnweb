using NoiThat_v2._0.Areas.Admin.Models;
using NoiThat_v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DBNoiThat = NoiThat_v2._0.Models.DBNoiThat;
using TaiKhoan = NoiThat_v2._0.Models.TaiKhoan;

namespace NoiThat_v2._0.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Email, string MatKhau)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                if (db.TaiKhoans.Where(p => p.Email == Email).FirstOrDefault() == null)
                {
                    return Json(new { success = false, message = "Email không tồn tại!" }, JsonRequestBehavior.AllowGet);
                }

                TaiKhoan tk = db.TaiKhoans.Where(p => p.Email == Email).FirstOrDefault();

                MatKhau = GetMD5(MatKhau + tk.Salt.ToString());

                if (MatKhau != tk.MatKhau)
                {
                    return Json(new { success = false, message = "Mật khẩu không đúng!" }, JsonRequestBehavior.AllowGet);
                }

                if (tk.IDQuyen == 1)
                {
                    Session.Add("admin", tk);
                    return Json(new { admin = true }, JsonRequestBehavior.AllowGet);
                }
                else Session.Add("user",tk);

                return Json(new { user = true }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public void Logout()
        {
            if (Session["user"] != null)
                Session.Remove("user");
            if (Session["admin"] != null)
                Session.Remove("admin");
        }

        [HttpPost]
        public ActionResult Sign_up(TaiKhoan tk)
        {
            using(DBNoiThat db = new DBNoiThat())
            {
                if (db.TaiKhoans.Where(p => p.Email == tk.Email).FirstOrDefault() != null)
                {
                    return Json(new { success = false, message = "Email đã tồn tại! Hãy thử đăng ký với một email khác!" }, JsonRequestBehavior.AllowGet);
                }

                Random r = new Random();
                tk.Salt = r.Next(100, 1000);
                tk.MatKhau = GetMD5(tk.MatKhau + tk.Salt.ToString());
                tk.XacNhanMatKhau = tk.MatKhau;
                tk.IDQuyen = 2;
                db.TaiKhoans.Add(tk);
                db.SaveChanges();

                tk = db.TaiKhoans.Where(p => p.Email == tk.Email).FirstOrDefault();

                Session.Add("user", tk);

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
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