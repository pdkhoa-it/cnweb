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
        public ActionResult Login(Login lg)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                if (db.TaiKhoans.Where(p => p.Email == lg.Email).FirstOrDefault() == null)
                {
                    return Json(new { success = false, message = "Email không tồn tại!" }, JsonRequestBehavior.AllowGet);
                }

                TaiKhoan tk = db.TaiKhoans.Where(p => p.Email == lg.Email).FirstOrDefault();

                lg.MatKhau = GetMD5(lg.MatKhau + tk.Salt.ToString());

                if (tk.MatKhau != lg.MatKhau)
                {
                    return Json(new { success = false, message = "Mật khẩu không đúng!" }, JsonRequestBehavior.AllowGet);
                }

                Session["ID"] = tk.ID;
                
                if (tk.IDQuyen == 1)
                {
                    Session["admin"] = tk.HoTen;
                    return Json(new { admin = true},JsonRequestBehavior.AllowGet);
                }

                Session["user"] = tk.HoTen;
                return Json(new{ user = true},JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public void Logout()
        {
            Session.Abandon();
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