using NoiThat_v2._0.Areas.Admin.Models;
using NoiThat_v2._0.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoiThat_v2._0.Areas.Admin.Controllers
{
    public class DanhMucController : Controller
    {
        // GET: DanhMuc
        public ActionResult Index()
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                var listNhom = db.NhomSanPhams.ToList();
                SelectList list = new SelectList(listNhom, "ID", "Ten");
                ViewBag.listNhom = list;
            }
            return View();
        }

        [HttpGet]
        public ActionResult GetDanhSach()
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                List<DanhMucViewModel> list = (from d in db.DanhMucSanPhams
                                               join n in db.NhomSanPhams on d.IDNhomSP equals n.ID
                                               select new DanhMucViewModel
                                               {
                                                   ID = d.ID,
                                                   Ten = d.Ten,
                                                   IDNhomSP = d.IDNhomSP,
                                                   TenNhom = n.Ten
                                               }).ToList();
                return Json(new { data = list }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult BeginEdit(int id)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                DanhMucViewModel n = (from p in db.DanhMucSanPhams.Where(c => c.ID == id)
                                      select new DanhMucViewModel
                                      {
                                          ID = p.ID,
                                          Ten = p.Ten,
                                          IDNhomSP = p.IDNhomSP
                                      }).FirstOrDefault();
                return Json(n, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(DanhMucSanPham d)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                if (d.ID == 0)
                {

                    d.Ten_slug = RemoveUnicode(d.Ten);

                    if (db.DanhMucSanPhams.Where(p => p.Ten_slug == d.Ten_slug).FirstOrDefault() != null)
                    {
                        return Json(new { success = false, message = "Tên hoặc tên không dấu của danh mục đã tồn tại!" }, JsonRequestBehavior.AllowGet);
                    }
                    NhomSanPham n = db.NhomSanPhams.Where(p => p.ID == d.IDNhomSP).FirstOrDefault();

                    string f = Server.MapPath(string.Format("~/storage/{0}/{1}", n.Ten_slug, d.Ten_slug));

                    Directory.CreateDirectory(f);

                    db.DanhMucSanPhams.Add(d);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Thêm mới thành công!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    try
                    {
                        //Đường dẫn cũ f1, đường dẫn mới f2

                        DanhMucSanPham dd = db.DanhMucSanPhams.Where(p => p.ID == d.ID).FirstOrDefault();
                        NhomSanPham nn = db.NhomSanPhams.Where(p => p.ID == dd.IDNhomSP).FirstOrDefault();
                        string f1 = Server.MapPath(string.Format("~/storage/{0}/{1}", nn.Ten_slug, dd.Ten_slug));

                        NhomSanPham n = db.NhomSanPhams.Where(p => p.ID == d.IDNhomSP).FirstOrDefault();
                        d.Ten_slug = RemoveUnicode(d.Ten);
                        string f2 = Server.MapPath(string.Format("~/storage/{0}/{1}", n.Ten_slug, d.Ten_slug));

                        Directory.Move(f1, f2);

                        dd.Ten = d.Ten;
                        dd.Ten_slug = d.Ten_slug;
                        dd.IDNhomSP = d.IDNhomSP;

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
                    DanhMucSanPham d = db.DanhMucSanPhams.Where(p => p.ID == id).FirstOrDefault();
                    NhomSanPham n = db.NhomSanPhams.Where(p => p.ID == d.IDNhomSP).FirstOrDefault();
                    string f = Server.MapPath(string.Format("~/storage/{0}/{1}", n.Ten_slug, d.Ten_slug));
                    Directory.Delete(f);

                    db.DanhMucSanPhams.Remove(d);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Xóa dữ liệu thành công!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { success = false, message = "Bạn không thể xóa trường này!" }, JsonRequestBehavior.AllowGet);
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
    }
}