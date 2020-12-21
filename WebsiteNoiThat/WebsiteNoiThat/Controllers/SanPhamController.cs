using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteNoiThat.Models;
using WebsiteNoiThat.ViewModels;

namespace WebsiteNoiThat.Controllers
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
                SelectList list2 = new SelectList(listNCC, "ID", "Ten");
                ViewBag.listNCC = list2;
            }
            return View(GetAllSanPham());
        }

        IEnumerable<SanPhamViewModel> GetAllSanPham()
        {
            List<SanPhamViewModel> list = new List<SanPhamViewModel>();
            using (DBNoiThat db = new DBNoiThat())
            {
                list = (from s in db.SanPhams
                        join n in db.NhaCungCaps on s.IDNCC equals n.ID
                        join d in db.DanhMucSanPhams on s.IDDanhMucSP equals d.ID
                        join nn in db.NhomSanPhams on d.IDNhomSP equals nn.ID
                        select new SanPhamViewModel
                        {
                            ID = s.ID,
                            Ten = s.Ten,
                            Mota = s.MoTa,
                            ImageSP = s.ImageSP,
                            IdDM = s.IDDanhMucSP,
                            TenDM = d.Ten,
                            TenNhom = nn.Ten,
                            IdNCC = s.IDNCC,
                            TenNCC = n.Ten
                        }).ToList();
            }
            return list;
        }

        [HttpPost]
        public string AddOrEdit(SanPham s)
        {
            
            if (s.ID == 0)
            {
                string filename = Path.GetFileNameWithoutExtension(s.ImageFile.FileName);
                string extention = Path.GetExtension(s.ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extention;
                s.ImageSP = "~/Images/" + filename;
                s.ImageFile.SaveAs(Path.Combine(Server.MapPath("~/Images/"), filename));
                using (DBNoiThat db = new DBNoiThat())
                {
                    db.SanPhams.Add(s);
                    db.SaveChanges();
                }
                return "Thêm thành công!";
            }
            else
            {
                using (DBNoiThat db = new DBNoiThat())
                {
                    SanPham s2 = db.SanPhams.Where(p => p.ID == s.ID).FirstOrDefault();
                    if (s.ImageFile != null)
                    {
                        string filename = Path.GetFileNameWithoutExtension(s.ImageFile.FileName);
                        string extention = Path.GetExtension(s.ImageFile.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extention;
                        s.ImageSP = "~/Images/" + filename;
                        s.ImageFile.SaveAs(Path.Combine(Server.MapPath("~/Images/"), filename));
                        s2.ImageSP = s.ImageSP;
                    }
                    
                    s2.Ten = s.Ten;
                    s2.MoTa = s.MoTa;
                    s2.IDDanhMucSP = s.IDDanhMucSP;
                    s2.IDNCC = s.IDNCC;
                    db.SaveChanges();
                }
                return "Sửa thành công!";
            }
        }

        [HttpPost]
        public string Delete(int id)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                SanPham s = db.SanPhams.Where(p => p.ID == id).FirstOrDefault();
                db.SanPhams.Remove(s);
                db.SaveChanges();
            }
            return "Xóa thành công!";
        }
    }
}