using NoiThat_v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoiThat_v2._0.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Cart()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddToCart(int ID)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                if (Session["cart"] == null)
                {
                    List<SanPhamCart> cart = new List<SanPhamCart>();
                    SanPhamCart sp = (from s in db.SanPhams
                                      join n in db.NhaCungCaps on s.IDNCC equals n.ID
                                      join d in db.DanhMucSanPhams on s.IDDanhMucSP equals d.ID
                                      join nh in db.NhomSanPhams on d.IDNhomSP equals nh.ID
                                      where (s.ID == ID)
                                      select new SanPhamCart
                                      {
                                          ID = s.ID,
                                          Ten = s.Ten,
                                          Gia = s.Gia,
                                          Mota = s.MoTa,
                                          PathImg = "/storage/" + nh.Ten_slug + "/" + d.Ten_slug + "/" + s.Ten_img
                                      }).FirstOrDefault();
                    sp.SoLuong = 1;
                    cart.Add(sp);
                    Session["cart"] = cart;

                    ThanhToan tt = new ThanhToan();
                    tt.TongTien = sp.Gia;
                    tt.GiamGia = 0;
                    tt.CanThanhToan = tt.TongTien;
                    Session["thanhtoan"] = tt;
                }
                else
                {
                    List<SanPhamCart> cart = (List<SanPhamCart>)Session["cart"];
                    SanPhamCart sp = cart.Where(p => p.ID == ID).FirstOrDefault();
                    if (sp != null)
                    {
                        sp.SoLuong = sp.SoLuong + 1;
                    }
                    else
                    {
                        sp = (from s in db.SanPhams
                              join n in db.NhaCungCaps on s.IDNCC equals n.ID
                              join d in db.DanhMucSanPhams on s.IDDanhMucSP equals d.ID
                              join nh in db.NhomSanPhams on d.IDNhomSP equals nh.ID
                              where (s.ID == ID)
                              select new SanPhamCart
                              {
                                  ID = s.ID,
                                  Ten = s.Ten,
                                  Gia = s.Gia,
                                  PathImg = "/storage/" + nh.Ten_slug + "/" + d.Ten_slug + "/" + s.Ten_img
                              }).FirstOrDefault();
                        sp.SoLuong = 1;
                        cart.Add(sp);
                    }

                    ThanhToan tt = (ThanhToan)Session["thanhtoan"];
                    tt.TongTien = tt.TongTien + sp.Gia;
                    tt.CanThanhToan = tt.TongTien - tt.GiamGia;

                    Session["thanhtoan"] = tt;
                }
            }
            return RedirectToAction("Index", "TrangChu");
        }

        [HttpPost]
        public ActionResult CartTang(int ID)
        {
            List<SanPhamCart> cart = (List<SanPhamCart>)Session["cart"];
            ThanhToan tt = (ThanhToan)Session["thanhtoan"];
            SanPhamCart sp = cart.Where(p => p.ID == ID).FirstOrDefault();
            if(sp.SoLuong <= 9)
            {
                sp.SoLuong = sp.SoLuong + 1;
                tt.TongTien = tt.TongTien + sp.Gia;
                tt.CanThanhToan = tt.TongTien - tt.GiamGia;
            }    
            return RedirectToAction("Cart", "Cart");
        }

        [HttpPost]
        public ActionResult CartGiam(int ID)
        {
            List<SanPhamCart> cart = (List<SanPhamCart>)Session["cart"];
            ThanhToan tt = (ThanhToan)Session["thanhtoan"];
            SanPhamCart sp = cart.Where(p => p.ID == ID).FirstOrDefault();
            if(sp.SoLuong > 2)
            {
                sp.SoLuong = sp.SoLuong - 1;
                tt.TongTien = tt.TongTien - sp.Gia;
                tt.CanThanhToan = tt.TongTien - tt.GiamGia;
            }    
            return RedirectToAction("Cart", "Cart");
        }

        [HttpPost]
        public ActionResult DeleteCart(int ID)
        {
            List<SanPhamCart> cart = (List<SanPhamCart>)Session["cart"];
            ThanhToan tt = (ThanhToan)Session["thanhtoan"];
            SanPhamCart sp = cart.Where(p => p.ID == ID).FirstOrDefault();
            tt.TongTien = tt.TongTien - sp.SoLuong * sp.Gia;
            tt.CanThanhToan = tt.TongTien - tt.GiamGia;
            cart.Remove(sp);
            return RedirectToAction("Index", "TrangChu");
        }

        [HttpPost]
        public ActionResult DeleteCart_Cart(int ID)
        {
            List<SanPhamCart> cart = (List<SanPhamCart>)Session["cart"];
            ThanhToan tt = (ThanhToan)Session["thanhtoan"];
            SanPhamCart sp = cart.Where(p => p.ID == ID).FirstOrDefault();
            tt.TongTien = tt.TongTien - sp.SoLuong * sp.Gia;
            tt.CanThanhToan = tt.TongTien - tt.GiamGia;
            cart.Remove(sp);
            return RedirectToAction("Cart", "Cart");
        }

        [HttpPost]
        public ActionResult GiamGia(string MaGG)
        {
            ThanhToan tt = (ThanhToan)Session["thanhtoan"];
            if (MaGG == "eshop" && tt.GiamGia == 0)
            {
                tt.GiamGia = 50;
                tt.CanThanhToan = tt.TongTien - tt.GiamGia;
            }    
            return RedirectToAction("Cart", "Cart");
        }
    }
}