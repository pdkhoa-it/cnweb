namespace NoiThat_v2._0.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Ten { get; set; }

        [Required]
        [StringLength(100)]
        public string Ten_slug { get; set; }

        [Required]
        [StringLength(100)]
        public string Ten_img { get; set; }

        public decimal Gia { get; set; }

        [Required]
        [StringLength(500)]
        public string MoTa { get; set; }

        public int IDNCC { get; set; }

        public int IDDanhMucSP { get; set; }
    }
}
