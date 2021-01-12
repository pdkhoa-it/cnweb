namespace NoiThat_v2._0.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonHang")]
    public partial class DonHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonHang()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên khách hàng!")]
        [StringLength(50)]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại!")]
        [StringLength(20)]
        public string Sdt { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email!")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thời gian!")]
        [StringLength(20)]
        public string ThoiGian { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ!")]
        [StringLength(100)]
        public string DiaChiGiaoHang { get; set; }

        public byte HinhThucThanhToan { get; set; }

        public byte TinhTrangThanhToan { get; set; }

        public byte TinhTrangGiaoHang { get; set; }

        public decimal TongTien { get; set; }

        public decimal DaGiamGia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
    }
}
