using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NoiThat.ViewModels
{
    public class TaiKhoanViewModel
    {
        public int ID { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public int? Salt { get; set; }
        public string Email { get; set; }
        public string HoTen { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public string Sdt { get; set; }
        public int? MaQuyen { get; set; }
        public string TenQuyen { get; set; }
        
    }
}