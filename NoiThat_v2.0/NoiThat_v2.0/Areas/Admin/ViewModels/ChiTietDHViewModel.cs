﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoiThat_v2._0.Areas.Admin.ViewModels
{
    public class ChiTietDHViewModel
    {
        public int IDCT { get; set; }
        public int IDSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string TenDanhMuc { get; set; }
        public string TenNhom { get; set; }
        public string TenNhaCungCap { get; set; }
        public int IDDonHang { get; set; }
        public decimal SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
    }
}