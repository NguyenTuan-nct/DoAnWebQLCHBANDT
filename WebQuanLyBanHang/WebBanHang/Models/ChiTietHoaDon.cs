using System.ComponentModel.DataAnnotations;

namespace NguyenTuanK55.Models
{
    public class ChiTietHoaDon
    {
        [Key]
        public int ChiTietHoaDonId { get; set; } 
        public int HoaDonId { get; set; } 
        public int SanPhamId { get; set; } 
        public int SoLuong { get; set; }
        public long Gia { get; set; }
        public HoaDon HoaDon { get; set; }
        public SanPham SanPham { get; set; }
    }
}
