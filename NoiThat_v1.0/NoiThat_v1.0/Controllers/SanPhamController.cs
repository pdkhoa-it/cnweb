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
                                                   PathImg = "/storage/" + nh.Ten_slug + "/" + d.Ten_slug + "/" + s.ImgPath
                                                }).ToList();
                return Json(new { data = list }, JsonRequestBehavior.AllowGet);
            }    
        }

        [HttpGet]
        public ActionResult BeginEdit(int id)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                SanPhamViewModel sp = (from s in db.SanPhams
                                      join n in db.NhaCungCaps on s.IDNCC equals n.ID
                                      join d in db.DanhMucSanPhams on s.IDDanhMucSP equals d.ID
                                      join nh in db.NhomSanPhams on d.IDNhomSP equals nh.ID
                                      where(s.ID == id)
                                      select new SanPhamViewModel
                                      {
                                          ID = s.ID,
                                          Ten = s.Ten,
                                          IDNCC = s.IDNCC,
                                          IDDM = s.IDDanhMucSP,
                                          Mota = s.MoTa
                                      }).FirstOrDefault();
                return Json(sp, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(SanPham s)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                s.Ten_slug = RemoveUnicode(s.Ten);

                DanhMucSanPham d = db.DanhMucSanPhams.Where(p => p.ID == s.IDDanhMucSP).FirstOrDefault();
                NhomSanPham n = db.NhomSanPhams.Where(p => p.ID == d.IDNhomSP).FirstOrDefault();

                if (s.ID == 0)
                {
                    try
                    {
                        if (s.ImgUpload != null)
                        {
                            //Lấy đuôi file ảnh
                            string ex = Path.GetExtension(s.ImgUpload.FileName);
                            //Lưu đường dẫn
                            s.ImgPath = s.Ten_slug + ex;
                            //Lưu file vào thư mục
                            s.ImgUpload.SaveAs(Path.Combine(Server.MapPath("~/storage/" + n.Ten_slug + "/" + d.Ten_slug + "/"), s.Ten_slug + ex));
                        }
                        else
                        {
                            //Copy và lưu file ảnh mặc định cho sản phẩm
                            System.IO.File.Copy(Server.MapPath("~/storage/default.png"), Server.MapPath("~/storage/" + n.Ten_slug + "/" + d.Ten_slug + "/" + s.Ten_slug + ".png"));
                            //Lưu đường dẫn
                            s.ImgPath = s.Ten_slug + ".png";
                        }

                        db.SanPhams.Add(s);
                        db.SaveChanges();

                        return Json(new { success = true, message = "Thêm mới thành công!" }, JsonRequestBehavior.AllowGet);
                    }
                    catch
                    {
                        return Json(new { success = false, message = "Thêm mới thất bại!" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {   
                        SanPham ss = db.SanPhams.Where(p => p.ID == s.ID).FirstOrDefault();
                        SanPhamViewModel sp = (from sEdit in db.SanPhams
                                               join dEdit in db.DanhMucSanPhams on s.IDDanhMucSP equals d.ID
                                               join nh in db.NhomSanPhams on d.IDNhomSP equals nh.ID
                                               where (sEdit.ID == s.ID)
                                               select new SanPhamViewModel
                                               {
                                                   ID = sEdit.ID,
                                                   Ten = sEdit.Ten,
                                                   IDDM = sEdit.IDDanhMucSP,
                                                   TenDM = dEdit.Ten,
                                                   TenNhom = nh.Ten,
                                                   PathImg = "~/storage/" + nh.Ten_slug + "/" + dEdit.Ten_slug + "/" + sEdit.ImgPath
                                               }).FirstOrDefault();

                        if(s.ImgUpload != null)
                        {
                            //Xóa ảnh cũ
                            System.IO.File.Delete(Server.MapPath(sp.PathImg));

                            //Lấy đuôi file ảnh
                            string ex = Path.GetExtension(s.ImgUpload.FileName);
                            //Lưu đường dẫn
                            s.ImgPath = s.Ten_slug + ex;
                            //Lưu file vào thư mục
                            s.ImgUpload.SaveAs(Path.Combine(Server.MapPath("~/storage/" + n.Ten_slug + "/" + d.Ten_slug + "/"), s.Ten_slug + ex));
                        }
                        else
                        {
                            //Lấy đuôi file ảnh
                            string ex = System.IO.Path.GetExtension(sp.PathImg);
                            //Copy và lưu file ảnh mặc định cho sản phẩm
                            System.IO.File.Copy(Server.MapPath(sp.PathImg), Server.MapPath("~/storage/" + n.Ten_slug + "/" + d.Ten_slug + "/" + s.Ten_slug + ex));
                            //Lưu đường dẫn
                            s.ImgPath = s.Ten_slug + ex;

                            //Xóa ảnh cũ
                            System.IO.File.Delete(Server.MapPath(sp.PathImg));
                        }

                        ss.Ten = s.Ten;
                        ss.Ten_slug = s.Ten_slug;
                        ss.MoTa = s.MoTa;
                        ss.IDDanhMucSP = s.IDDanhMucSP;
                        ss.IDNCC = ss.IDNCC;
                        ss.ImgPath = s.ImgPath;
                        ss.ImgUpload = s.ImgUpload;

                        db.SaveChanges();
                        return Json(new { success = true, message = "Cập nhật thành công!" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                try
                {
                    SanPhamViewModel sp = (from s in db.SanPhams
                                           join d in db.DanhMucSanPhams on s.IDDanhMucSP equals d.ID
                                           join nh in db.NhomSanPhams on d.IDNhomSP equals nh.ID
                                           where (s.ID == id)
                                           select new SanPhamViewModel
                                           {
                                               ID = s.ID,
                                               Ten = s.Ten,
                                               IDDM = s.IDDanhMucSP,
                                               TenDM = d.Ten,
                                               TenNhom = nh.Ten,
                                               PathImg = "~/storage/" + nh.Ten_slug + "/" + d.Ten_slug + "/" + s.ImgPath
                                           }).FirstOrDefault();


                    //Xóa file ảnh
                    System.IO.File.Delete(Server.MapPath(sp.PathImg));

                    SanPham ss = db.SanPhams.Where(p => p.ID == id).FirstOrDefault();

                    db.SanPhams.Remove(ss);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Xóa dữ liệu thành công!" }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(new { success = false, message = "Xóa dữ liệu thất bại!" }, JsonRequestBehavior.AllowGet);
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