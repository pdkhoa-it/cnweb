namespace NoiThat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Hinh")]
    public partial class Hinh
    {
        public int ID { get; set; }

        public int? IDSanPham { get; set; }

        [StringLength(300)]
        public string DuongDan { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
