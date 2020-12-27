namespace NoiThat_v1._0.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhaCungCap")]
    public partial class NhaCungCap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhaCungCap()
        {
            SanPhams = new HashSet<SanPham>();
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "Tên nhà cung cấp không được bỏ trống!")]
        [StringLength(100)]
        public string Ten { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được bỏ trống!")]
        [StringLength(100)]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được bỏ trống!")]
        [StringLength(20)]
        public string SoDienThoai { get; set; }

        [Required(ErrorMessage = "Email không được bỏ trống!")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mã số thuế không được bỏ trống!")]
        [StringLength(100)]
        public string MaSoThue { get; set; }

        [Required(ErrorMessage = "Số tài khoản không được bỏ trống!")]
        [StringLength(100)]
        public string SoTaiKhoan { get; set; }

        [Required(ErrorMessage = "Người đại diện không được bỏ trống!")]
        [StringLength(100)]
        public string NguoiDaiDien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
