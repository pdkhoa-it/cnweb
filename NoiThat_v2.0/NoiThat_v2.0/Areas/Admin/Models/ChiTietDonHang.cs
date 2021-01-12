namespace NoiThat_v2._0.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietDonHang")]
    public partial class ChiTietDonHang
    {
        [Key]
        public int IDCT { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn sản phẩm!")]
        public int IDSanPham { get; set; }

        public int IDDonHang { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng sản phẩm!")]
        public decimal SoLuong { get; set; }

        public decimal DonGia { get; set; }

        public decimal ThanhTien { get; set; }

        public virtual DonHang DonHang { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
