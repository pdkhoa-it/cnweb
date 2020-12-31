using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoiThat_v1._0.ViewModels
{
    public class DonHangViewModel
    {
        public int ID { get; set; }
        public string HoTen { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public byte HinhThucThanhToan { get; set; }
        public string ThoiGian { get; set; }
        public string DiaChiGiaoHang { get; set; }
        public byte TinhTrangThanhToan { get; set; }
        public byte TinhTrangGiaoHang { get; set; }
        public decimal TongTien { get; set; }
    }
}