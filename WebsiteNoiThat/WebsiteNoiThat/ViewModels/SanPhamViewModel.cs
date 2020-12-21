using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebsiteNoiThat.ViewModels
{
    public class SanPhamViewModel
    {
        public int ID { get; set; }
        public string Ten { get; set; }
        public string Mota { get; set; }
        public string ImageSP { get; set; }
        public int? IdNCC { get; set; }
        public string TenNCC { get; set; }
        public int? IdDM { get; set; }
        public string TenDM { get; set; }
        public string TenNhom { get; set; }
    }
}