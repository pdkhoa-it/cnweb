﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoiThat_v2._0.Areas.Admin.ViewModels
{
    public class SanPhamViewModel
    {
        public int ID { get; set; }
        public string Ten { get; set; }
        public string Ten_img { get; set; }
        public int IDNCC { get; set; }
        public string TenNCC { get; set; }
        public int IDDM { get; set; }
        public string TenDM { get; set; }
        public string TenNhom { get; set; }
        public decimal Gia { get; set; }
        public string Mota { get; set; }
        public string PathImg { get; set; }
    }
}