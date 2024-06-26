using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NguyenTuanK55.Models
{
    public partial class KhoHang
    {
        [Key]
        public int KhoHangId { get; set; }
        //public string MaKho { get; set; }
        public string TenKho { get; set; }
        public string DiaChiKho { get; set; }
        public int SoLuongTon { get; set; }
        public ICollection<SanPham> SanPhams { get; set; }
    }
}