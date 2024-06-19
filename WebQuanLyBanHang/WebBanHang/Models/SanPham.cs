using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NguyenTuanK55.Models
{
    public partial class SanPham
    {
        [Key]
        public int SanPhamId { get; set; }
        public string LoaiSanPham { get; set; }
        public string TenSanPham { get; set; }
        public long GiaSanPham { get; set; }
        public string? Anh { get; set; }
        public DateTime NgayNhap { get; set; }
        public int TonKho { get; set; }
        public string? MoTa { get; set; }
        public ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}