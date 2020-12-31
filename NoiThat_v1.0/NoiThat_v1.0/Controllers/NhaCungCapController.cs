using NoiThat_v1._0.Models;
using NoiThat_v1._0.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoiThat_v1._0.Controllers
{
    public class NhaCungCapController : Controller
    {
        // GET: NhaCungCap
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetDanhSach()
        {
            using(DBNoiThat db = new DBNoiThat())
            {
                List<NhaCungCapViewModel> list = (from n in db.NhaCungCaps
                                                  select new NhaCungCapViewModel {
                                                        ID = n.ID,
                                                        Ten = n.Ten,
                                                        SoDienThoai = n.SoDienThoai,
                                                        Email = n.Email,
                                                        DiaChi = n.DiaChi,
                                                        MaSoThue = n.MaSoThue,
                                                        SoTaiKhoan = n.SoTaiKhoan,
                                                        NguoiDaiDien = n.NguoiDaiDien
                                                  }).ToList();
                return Json(new { data = list }, JsonRequestBehavior.AllowGet);
            }    
        }

        [HttpGet]
        public ActionResult BeginEdit(int id)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                NhaCungCapViewModel n = (from t in db.NhaCungCaps.Where(p => p.ID == id)
                                        select new NhaCungCapViewModel
                                        {
                                            ID = t.ID,
                                            Ten = t.Ten,
                                            Email = t.Email,
                                            DiaChi = t.DiaChi,
                                            SoDienThoai = t.SoDienThoai,
                                            MaSoThue = t.MaSoThue,
                                            SoTaiKhoan = t.SoTaiKhoan,
                                            NguoiDaiDien = t.NguoiDaiDien
                                        }).FirstOrDefault();
                return Json(n, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(NhaCungCap n)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                if (n.ID == 0)
                {
                    if (db.NhaCungCaps.Where(p => p.Ten == n.Ten).FirstOrDefault() != null)
                    {
                        return Json(new { success = false, message = "Tên nhà cung cấp đã bị trùng!" }, JsonRequestBehavior.AllowGet);
                    }

                    db.NhaCungCaps.Add(n);
                    db.SaveChanges();

                    return Json(new { success = true, message = "Thêm mới thành công!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    try
                    {
                        db.Entry(n).State = EntityState.Modified;
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
                    NhaCungCap n = db.NhaCungCaps.Where(p => p.ID == id).FirstOrDefault();
                    db.NhaCungCaps.Remove(n);
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