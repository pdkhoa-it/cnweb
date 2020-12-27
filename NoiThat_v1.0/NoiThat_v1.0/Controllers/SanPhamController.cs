using NoiThat_v1._0.Models;
using NoiThat_v1._0.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoiThat_v1._0.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        public ActionResult Index()
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                var listDM = db.DanhMucSanPhams.ToList();
                SelectList list = new SelectList(listDM, "ID", "Ten");
                ViewBag.listDM = list;

                var listNCC = db.NhaCungCaps.ToList();
                list = new SelectList(listNCC, "ID", "Ten");
                ViewBag.listNCC = list;
            }
            return View();
        }

        [HttpGet]
        public ActionResult GetDanhSach()
        {
            using(DBNoiThat db = new DBNoiThat())
            {
                List<SanPhamViewModel> list = (from s in db.SanPhams
                                               join n in db.NhaCungCaps on s.IDNCC equals n.ID
                                               join d in db.DanhMucSanPhams on s.IDDanhMucSP equals d.ID
                                               join nh in db.NhomSanPhams on d.IDNhomSP equals nh.ID
                                               select new SanPhamViewModel {
                                                    ID = s.ID,
                                                    Ten = s.Ten,
                                                    IDNCC = s.IDNCC,
                                                    TenNCC = n.Ten,
                                                    IDDM = s.IDDanhMucSP,
                                                    TenDM = d.Ten,
                                                    TenNhom = nh.Ten,
                                                    ImgPath = s.ImgPath
                                               }).ToList();
                return Json(new { data = list }, JsonRequestBehavior.AllowGet);
            }    
        }

        [HttpPost]
        public ActionResult AddOrEdit(SanPham s)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                if (s.ID == 0)
                {
                    try
                    {
                        s.Ten_slug = RemoveUnicode(s.Ten);

                        if(s.ImgUpload != null)
                        {
                            DanhMucSanPham d = db.DanhMucSanPhams.Where(p => p.ID == s.IDDanhMucSP).FirstOrDefault();
                            NhomSanPham n = db.NhomSanPhams.Where(p => p.ID == d.IDNhomSP).FirstOrDefault();

                            string ex = Path.GetExtension(s.ImgUpload.FileName);

                            s.ImgPath = "/storage/" + n.Ten_slug + "/" + d.Ten_slug + "/" + s.Ten_slug + ex;
                            s.ImgUpload.SaveAs(Path.Combine(Server.MapPath("/storage/" + n.Ten_slug + "/" + d.Ten_slug + "/"), s.Ten_slug + ex));
                        }
                        else
                        {
                            s.ImgPath = "~/storage/default.png";
                        }

                        db.SanPhams.Add(s);
                        db.SaveChanges();

                        return Json(new { success = true, message = "Thêm mới thành công!" }, JsonRequestBehavior.AllowGet);
                    }
                    catch
                    {
                        return Json(new { success = false, message = "Thêm thông thành công!" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    try
                    {

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