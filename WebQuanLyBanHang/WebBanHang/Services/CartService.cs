using Microsoft.EntityFrameworkCore;
using NguyenTuanK55.Models;
using System.Threading.Tasks;

namespace NguyenTuanK55.Services
{
    public class CartService
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddToCart(int sanPhamId)
        {
            var existingCartItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.SanPhamId == sanPhamId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += 1;
            }
            else
            {
                var newCartItem = new CartItem
                {
                    SanPhamId = sanPhamId,
                    Quantity = 1
                };
                _context.CartItems.Add(newCartItem);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<int> GetCartItemCount()
        {
            return await _context.CartItems.SumAsync(c => c.Quantity);
        }
    }
}
