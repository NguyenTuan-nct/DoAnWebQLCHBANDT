using System.ComponentModel.DataAnnotations;

namespace NguyenTuanK55.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }
        public int Quantity { get; set; }
        public int SanPhamId { get; set; }
        public SanPham SanPham { get; set; }
    }
}
