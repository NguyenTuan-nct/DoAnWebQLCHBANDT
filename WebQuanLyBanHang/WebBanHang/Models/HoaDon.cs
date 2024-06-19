using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NguyenTuanK55.Models
{
    public class HoaDon
    {
        [Key]
        public int HoaDonId { get; set; }
        public int NguoiDungId { get; set; } // Changed UserId to NguoiDungId
        public DateTime NgayLap { get; set; }
        public string TenKhachHang { get; set; } // Added property
        public string SoDienThoai { get; set; } // Added property
        public NguoiDung NguoiDung { get; set; }
        public ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
