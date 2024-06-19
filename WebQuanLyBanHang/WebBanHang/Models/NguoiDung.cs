using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NguyenTuanK55.Models
{
    public partial class NguoiDung
    {
        [Key]
        public int NguoiDungId { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string MatKhau { get; set; }
        public int Cap { get; set; }
        public ICollection<HoaDon> HoaDons { get; set; }
    }
}
