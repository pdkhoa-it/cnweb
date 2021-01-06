﻿using NoiThat_v2._0.Models;
using NoiThat_v2._0.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace NoiThat_v2._0.Controllers
{
    public class TrangChuController : Controller
    {
        // GET: TrangChu
        public ActionResult Index(int? page)
        {
            using (DBNoiThat db = new DBNoiThat())
            {
                var list = (from s in db.SanPhams
                                               join n in db.NhaCungCaps on s.IDNCC equals n.ID
                                               join d in db.DanhMucSanPhams on s.IDDanhMucSP equals d.ID
                                               join nh in db.NhomSanPhams on d.IDNhomSP equals nh.ID
                                               select new SanPhamViewModel
                                               {
                                                   ID = s.ID,
                                                   Ten = s.Ten,
                                                   Gia = s.Gia,
                                                   Ten_img = s.Ten_img,
                                                   PathImg = "/storage/" + nh.Ten_slug + "/" + d.Ten_slug + "/" + s.Ten_img
                                               }).OrderByDescending(p => p.ID).ToPagedList(page ?? 1,8);
                return View(list);
            }
        }

    }
}