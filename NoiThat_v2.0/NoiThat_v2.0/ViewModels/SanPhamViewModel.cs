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
        public string Ten_img { get; set; }
        public decimal Gia { get; set; }
        public string PathImg { get; set; }
    }
}