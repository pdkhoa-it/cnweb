using NoiThat_v2._0.Areas.Admin.Models;
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
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddDonHang(DonHang dh, string AddTaiKhoan)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                try
                {
                    dh.ThoiGian = DateTime.Now.ToString("dd/MM/yyyy");
                    dh.TinhTrangThanhToan = 0;
                    dh.TinhTrangGiaoHang = 0;

                    ThanhToan tt = (ThanhToan)Session["thanhtoan"];
                    dh.TongTien = tt.CanThanhToan;
                    dh.DaGiamGia = tt.GiamGia;

                    db.DonHangs.Add(dh);
                    db.SaveChanges();

                    foreach (SanPhamCart item in (List<SanPhamCart>)Session["cart"])
                    {
                        ChiTietDonHang ct = new ChiTietDonHang();
                        ct.IDSanPham = item.ID;
                        ct.IDDonHang = db.DonHangs.OrderByDescending(p => p.ID).FirstOrDefault().ID;
                        ct.SoLuong = item.SoLuong;
                        ct.DonGia = item.Gia;
                        ct.ThanhTien = item.SoLuong * item.Gia;

                        db.ChiTietDonHangs.Add(ct);
                        db.SaveChanges();
                    }  
                    
                    if(AddTaiKhoan == "Add" && db.TaiKhoans.Where(p => p.Email == dh.Email).FirstOrDefault() == null)
                    {
                        DBNoiThat_User db_u = new DBNoiThat_User();
                        NoiThat_v2._0.Models.TaiKhoan tk = new NoiThat_v2._0.Models.TaiKhoan();
                        tk.Email = dh.Email;
                        tk.HoTen = dh.HoTen;
                        tk.DiaChi = dh.DiaChiGiaoHang;
                        tk.Sdt = dh.Sdt;

                        Random r = new Random();
                        tk.Salt = r.Next(100, 1000);
                        tk.MatKhau = GetMD5(dh.Sdt + tk.Salt.ToString());
                        tk.XacNhanMatKhau = tk.MatKhau;
                        tk.IDQuyen = 2;
                        db_u.TaiKhoans.Add(tk);
                        db.SaveChanges();

                        tk = db_u.TaiKhoans.Where(p => p.Email == tk.Email).FirstOrDefault();

                        Session.Add("user", tk);
                    }

                    Session.Remove("cart");
                    Session.Remove("thanhtoan");

                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
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