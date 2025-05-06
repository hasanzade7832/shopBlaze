using kargardoon.Data;

namespace kargardoon.Repository.IRepository
{
    public interface IShoppingCartRepository
    {
        // 🟡 افزودن یا کاهش آیتم از سبد خرید (بر اساس تغییر در تعداد)
        Task<bool> UpdateCartAsync(string userId, int productId, int countChange);

        // 🔵 دریافت لیست سبد خرید برای یک کاربر خاص
        Task<IEnumerable<ShoppingCart>> GetAllAsync(string userId);

        // 🔴 پاک کردن سبد خرید برای کاربر خاص
        Task<bool> ClearCartAsync(string userId);
    }
}
