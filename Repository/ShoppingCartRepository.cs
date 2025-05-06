using kargardoon.Data;
using kargardoon.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace kargardoon.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;

        public ShoppingCartRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> ClearCartAsync(string userId)
        {
            var cartItems = await _db.ShoppingCart
                .Where(u => u.ApplicationUserId == userId)
                .ToListAsync();

            _db.ShoppingCart.RemoveRange(cartItems);

            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<ShoppingCart>> GetAllAsync(string userId)
        {
            return await _db.ShoppingCart
                .Where(u => u.ApplicationUserId == userId)
                .Include(u => u.Product)
                .ToListAsync();
        }

        public async Task<bool> UpdateCartAsync(string userId, int productId, int countChange)
        {
            if (string.IsNullOrEmpty(userId))
                return false;

            var cart = await _db.ShoppingCart
                .FirstOrDefaultAsync(u => u.ApplicationUserId == userId && u.ProductId == productId);

            if (cart == null)
            {
                var newCart = new ShoppingCart
                {
                    ApplicationUserId = userId,
                    ProductId = productId,
                    Count = countChange
                };

                await _db.ShoppingCart.AddAsync(newCart);
            }
            else
            {
                cart.Count += countChange;

                if (cart.Count <= 0)
                {
                    _db.ShoppingCart.Remove(cart);
                }
            }

            return await _db.SaveChangesAsync() > 0;
        }
    }
}
