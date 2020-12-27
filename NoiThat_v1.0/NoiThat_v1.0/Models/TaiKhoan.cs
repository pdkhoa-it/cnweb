namespace NoiThat_v1._0.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaiKhoan()
        {
            DonHangs = new HashSet<DonHang>();
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "Email không được bỏ trống!")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage ="Mật khẩu không được bỏ trống!")]
        [MinLength(4,ErrorMessage ="Mật khẩu phải có ít nhất 4 ký tự!")]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Xác nhận mật không được bỏ trống!")]
        [Compare("MatKhau")]
        public string XacNhanMatKhau { get; set; }

        public int Salt { get; set; }

        [Required(ErrorMessage = "Họ và tên không được bỏ trống!")]
        [StringLength(50)]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Ngày sinh không được bỏ trống!")]
        [StringLength(20)]
        public string NgaySinh { get; set; }

        [Required(ErrorMessage = "Giới tính không được bỏ trống!")]
        [StringLength(3)]
        public string GioiTinh { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được bỏ trống!")]
        [StringLength(100)]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được bỏ trống!")]
        [StringLength(20)]
        public string Sdt { get; set; }

        public int IDQuyen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonHang> DonHangs { get; set; }

        public virtual PhanQuyen PhanQuyen { get; set; }
    }
}
