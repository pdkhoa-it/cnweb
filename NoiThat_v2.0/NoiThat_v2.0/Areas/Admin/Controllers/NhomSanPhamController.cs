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
    public class NhomSanPhamController : Controller
    {
        // GET: NhomSanPham
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetDanhSach()
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                List<NhomSanPhamViewModel> list = (from n in db.NhomSanPhams
                                                   select new NhomSanPhamViewModel
                                                   {
                                                       ID = n.ID,
                                                       Ten = n.Ten
                                                   }).ToList();
                return Json(new { data = list }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult BeginEdit(int id)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                NhomSanPhamViewModel n = (from p in db.NhomSanPhams.Where(c => c.ID == id)
                                          select new NhomSanPhamViewModel { ID = p.ID, Ten = p.Ten }).FirstOrDefault();
                return Json(n, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(NhomSanPham n)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                if (n.ID == 0)
                {
                    n.Ten_slug = RemoveUnicode(n.Ten);

                    if (db.NhomSanPhams.Where(p => p.Ten_slug == n.Ten_slug).FirstOrDefault() != null)
                    {
                        return Json(new { success = false, message = "Tên hoặc tên không dấu của nhóm này đã tồn tại!" }, JsonRequestBehavior.AllowGet);
                    }

                    string f = Server.MapPath(string.Format("~/storage/{0}", n.Ten_slug));

                    Directory.CreateDirectory(f);

                    db.NhomSanPhams.Add(n);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Thêm mới thành công!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    try
                    {
                        NhomSanPham nn = db.NhomSanPhams.Where(p => p.ID == n.ID).FirstOrDefault();
                        string f1 = Server.MapPath(string.Format("~/storage/{0}", nn.Ten_slug));

                        n.Ten_slug = RemoveUnicode(n.Ten);
                        string f2 = Server.MapPath(string.Format("~/storage/{0}", n.Ten_slug));

                        Directory.Move(f1, f2);

                        nn.Ten = n.Ten;
                        nn.Ten_slug = n.Ten_slug;

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
                    NhomSanPham n = db.NhomSanPhams.Where(p => p.ID == id).FirstOrDefault();

                    string f = Server.MapPath(string.Format("~/storage/{0}", n.Ten_slug));
                    Directory.Delete(f);

                    db.NhomSanPhams.Remove(n);
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