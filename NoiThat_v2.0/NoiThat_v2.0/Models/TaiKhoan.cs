namespace NoiThat_v2._0.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email!")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu!")]
        [MinLength(4,ErrorMessage = "Mật khẩu phải có ít nhất 4 ký tự!")]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu!")]
        [Compare("MatKhau")]
        public string XacNhanMatKhau { get; set; }

        public int Salt { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ và tên!")]
        [StringLength(50)]
        public string HoTen { get; set; }

        [StringLength(20)]
        public string NgaySinh { get; set; }

        [StringLength(3)]
        public string GioiTinh { get; set; }

        [StringLength(100)]
        public string DiaChi { get; set; }

        [StringLength(20)]
        public string Sdt { get; set; }

        public int IDQuyen { get; set; }
    }
}
