namespace NoiThat_v2._0.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm!")]
        [StringLength(100)]
        public string Ten { get; set; }

        [Required]
        [StringLength(100)]
        public string Ten_slug { get; set; }

        [Required]
        [StringLength(100)]
        public string Ten_img { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá sản phẩm!")]
        public decimal Gia { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mô tả sản phẩm!")]
        [StringLength(500)]
        public string MoTa { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn nhà cung cấp!")]
        public int IDNCC { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh mục!")]
        public int IDDanhMucSP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        public virtual DanhMucSanPham DanhMucSanPham { get; set; }

        public virtual NhaCungCap NhaCungCap { get; set; }
        
        [NotMapped]
        public HttpPostedFileBase ImgUpload { get; set; }
    }
}
