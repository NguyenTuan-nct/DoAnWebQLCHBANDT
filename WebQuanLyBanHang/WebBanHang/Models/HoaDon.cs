using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NguyenTuanK55.Models
{
    public class HoaDon
    {
        [Key]
        public int HoaDonId { get; set; }
        public int NguoiDungId { get; set; }
        public int KhachHangId { get; set; }
        public DateTime NgayLap { get; set; }
        public NguoiDung NguoiDung { get; set; }
        public KhachHang KhachHang { get; set; }
        public ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
