using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoiThat_v2._0.ViewModels
{
    public class SanPhamViewModel
    {
        public int ID { get; set; }
        public string Ten { get; set; }
        public string TenDanhMuc { get; set; }
        public string TenNhom { get; set; }
        public string TenNCC { get; set; }
        public string Mota { get; set; }
        public decimal Gia { get; set; }
        public int IDNhom { get; set; }
        public string PathImg { get; set; }
    }
}