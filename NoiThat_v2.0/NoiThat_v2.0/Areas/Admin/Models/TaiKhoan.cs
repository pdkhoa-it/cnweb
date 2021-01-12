namespace NoiThat_v2._0.Areas.Admin.Models
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
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu!")]
        [Compare("MatKhau", ErrorMessage ="Xác nhận mật khẩu không đúng!")]
        public string XacNhanMatKhau { get; set; }

        public int Salt { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ và tên!")]
        [StringLength(50)]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày sinh!")]
        [StringLength(20)]
        public string NgaySinh { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giới tính!")]
        [StringLength(3)]
        public string GioiTinh { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ!")]
        [StringLength(100)]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại!")]
        [StringLength(20)]
        public string Sdt { get; set; }

        public int IDQuyen { get; set; }

        public virtual PhanQuyen PhanQuyen { get; set; }
    }
}
