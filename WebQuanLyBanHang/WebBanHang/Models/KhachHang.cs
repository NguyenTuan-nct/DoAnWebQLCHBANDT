using System.ComponentModel.DataAnnotations;

namespace NguyenTuanK55.Models
{
    public class KhachHang
    {
        [Key]
        public int KhachHangId { get; set; }
        public string TenKhachHang { get; set; }
        public DateTime? NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public ICollection<HoaDon> HoaDons { get; set; }
    }
}
