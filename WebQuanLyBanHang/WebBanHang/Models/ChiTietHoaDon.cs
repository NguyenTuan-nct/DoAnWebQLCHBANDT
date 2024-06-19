using System.ComponentModel.DataAnnotations;

namespace NguyenTuanK55.Models
{
    public class ChiTietHoaDon
    {
        [Key]
        public int ChiTietHoaDonId { get; set; } // Changed IDChiTietHoaDon to ChiTietHoaDonId
        public int HoaDonId { get; set; } // Changed IDHoaDon to HoaDonId
        public int SanPhamId { get; set; } // Changed IDSanPham to SanPhamId
        public int SoLuong { get; set; }
        public long Gia { get; set; }
        public HoaDon HoaDon { get; set; }
        public SanPham SanPham { get; set; }
    }
}

