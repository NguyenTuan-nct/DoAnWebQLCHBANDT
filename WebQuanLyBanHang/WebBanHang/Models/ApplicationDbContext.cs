using Microsoft.EntityFrameworkCore;

namespace NguyenTuanK55.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<SanPham> SanPhams { get; set; }

        public DbSet<NguoiDung> NguoiDungs { get; set; }

        public DbSet<HoaDon> HoaDons { get; set; }

        public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.ToTable("SanPham");
                entity.Property(e => e.SanPhamId).HasColumnName("SanPhamId");
                entity.Property(e => e.TenSanPham).IsRequired().HasMaxLength(500);
                entity.Property(e => e.LoaiSanPham).IsRequired().HasMaxLength(500); 
                entity.Property(e => e.GiaSanPham).IsRequired();
                entity.Property(e => e.NgayNhap).HasColumnType("date");
                entity.Property(e => e.MoTa).HasColumnType("ntext"); 
            });

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.ToTable("NguoiDung");
                entity.HasKey(e => e.NguoiDungId); 
                entity.Property(e => e.HoTen).IsRequired().HasMaxLength(200); 
                entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
                entity.Property(e => e.DienThoai).IsRequired().HasMaxLength(20); 
                entity.Property(e => e.MatKhau).IsRequired().HasMaxLength(128).IsUnicode(false); 
                entity.Property(e => e.NgaySinh).HasColumnType("date"); 
                entity.Property(e => e.DiaChi).HasMaxLength(500);
                entity.Property(e => e.Cap).IsRequired();
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.ToTable("HoaDon");
                entity.HasKey(e => e.HoaDonId); 
                entity.Property(e => e.NgayLap).HasColumnType("date");
                entity.Property(e => e.TenKhachHang).IsRequired().HasMaxLength(200); 
                entity.Property(e => e.SoDienThoai).HasMaxLength(20); 

                entity.HasOne(e => e.NguoiDung)
                    .WithMany(d => d.HoaDons)
                    .HasForeignKey(e => e.NguoiDungId);
            });

            modelBuilder.Entity<ChiTietHoaDon>(entity =>
            {
                entity.ToTable("ChiTietHoaDon");
                entity.HasKey(e => e.ChiTietHoaDonId); 

                entity.HasOne(e => e.HoaDon)
                    .WithMany(h => h.ChiTietHoaDons)
                    .HasForeignKey(e => e.HoaDonId); 

                entity.HasOne(e => e.SanPham)
                    .WithMany(p => p.ChiTietHoaDons)
                    .HasForeignKey(e => e.SanPhamId); 
            });
        }
    }
}
