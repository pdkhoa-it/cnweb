namespace WebsiteNoiThat.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBNoiThat : DbContext
    {
        public DBNoiThat()
            : base("name=DBNoiThat")
        {
        }

        public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual DbSet<DanhMucSanPham> DanhMucSanPhams { get; set; }
        public virtual DbSet<DonHang> DonHangs { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public virtual DbSet<NhomSanPham> NhomSanPhams { get; set; }
        public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietDonHang>()
                .Property(e => e.SoLuong)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ChiTietDonHang>()
                .Property(e => e.DonGia)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ChiTietDonHang>()
                .Property(e => e.ThanhTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DanhMucSanPham>()
                .HasMany(e => e.SanPhams)
                .WithOptional(e => e.DanhMucSanPham)
                .HasForeignKey(e => e.IDDanhMucSP);

            modelBuilder.Entity<DonHang>()
                .Property(e => e.NgayThang)
                .IsUnicode(false);

            modelBuilder.Entity<DonHang>()
                .HasMany(e => e.ChiTietDonHangs)
                .WithOptional(e => e.DonHang)
                .HasForeignKey(e => e.IDDonHang);

            modelBuilder.Entity<NhaCungCap>()
                .HasMany(e => e.SanPhams)
                .WithOptional(e => e.NhaCungCap)
                .HasForeignKey(e => e.IDNCC);

            modelBuilder.Entity<NhomSanPham>()
                .HasMany(e => e.DanhMucSanPhams)
                .WithOptional(e => e.NhomSanPham)
                .HasForeignKey(e => e.IDNhomSP);

            modelBuilder.Entity<PhanQuyen>()
                .HasMany(e => e.TaiKhoans)
                .WithOptional(e => e.PhanQuyen)
                .HasForeignKey(e => e.MaQuyen);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.ImageSP)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietDonHangs)
                .WithOptional(e => e.SanPham)
                .HasForeignKey(e => e.IDSanPham);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.TenDangNhap)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.NgaySinh)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.Sdt)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.DonHangs)
                .WithOptional(e => e.TaiKhoan)
                .HasForeignKey(e => e.IDTaiKhoan);
        }
    }
}
