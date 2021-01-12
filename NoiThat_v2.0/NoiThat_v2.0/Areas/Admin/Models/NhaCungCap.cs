namespace NoiThat_v2._0.Areas.Admin.Models
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

        [Required(ErrorMessage = "Vui lòng nhập tên nhà cung cấp!")]
        [StringLength(100)]
        public string Ten { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ!")]
        [StringLength(100)]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại!")]
        [StringLength(20)]
        public string SoDienThoai { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email!")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mã số thuế!")]
        [StringLength(100)]
        public string MaSoThue { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số tài khoản!")]
        [StringLength(100)]
        public string SoTaiKhoan { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập người đại diện!")]
        [StringLength(100)]
        public string NguoiDaiDien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
