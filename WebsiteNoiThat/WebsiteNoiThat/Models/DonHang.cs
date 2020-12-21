namespace WebsiteNoiThat.Models
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

        public int? IDTaiKhoan { get; set; }

        [StringLength(50)]
        public string HinhThucThanhToan { get; set; }

        [StringLength(20)]
        public string NgayThang { get; set; }

        [StringLength(100)]
        public string DiaChiGiaoHang { get; set; }

        public byte? TinhTrangThanhToan { get; set; }

        public byte? TinhTrangGiaoHang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
