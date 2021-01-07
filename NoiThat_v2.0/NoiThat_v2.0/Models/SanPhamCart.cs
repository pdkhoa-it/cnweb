using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoiThat_v2._0.Models
{
    public class SanPhamCart
    {
        public int ID { get; set; }
        public string Ten { get; set; }
        public decimal Gia { get; set; }
        public int SoLuong { get; set; }
        public string PathImg { get; set; }
        public string Mota { get; set; }
    }
}