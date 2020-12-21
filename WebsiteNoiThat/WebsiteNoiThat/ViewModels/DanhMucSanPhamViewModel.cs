using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteNoiThat.ViewModels
{
    public class DanhMucSanPhamViewModel
    {
        public int ID { get; set; }
        public string Ten { get; set; }
        public int? NhomSP_ID { get; set; }
        public string TenNhomSP { get; set; }
    }
}