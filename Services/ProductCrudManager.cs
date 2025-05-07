using kargardoon.Data;
using kargardoon.Repository.IRepository;

namespace kargardoon.Services
{
    public class ProductCrudManager
    {
        private readonly IProductRepository _repo;
        private readonly NotificationService _notifier;

        public ProductCrudManager(IProductRepository repo, NotificationService notifier)
        {
            _repo = repo;
            _notifier = notifier;
        }

        public async Task<List<Product>> LoadProductsAsync()
        {
            var result = await _repo.GetAllAsync(); // ✅ استفاده امن از EF با DbContextFactory
            return result.ToList();
        }

        public async Task<bool> SaveAsync(Product product)
        {
            if (product.Id == 0)
            {
                await _repo.CreateAsync(product);
                _notifier.Notify("Product added!", NotificationLevel.Success);
                return true;
            }
            else
            {
                await _repo.UpdateAsync(product);
                _notifier.Notify("Product updated!", NotificationLevel.Success);
                return false;
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
            _notifier.Notify("Product deleted!", NotificationLevel.Warning);
        }

        public void NotifyCancelDelete()
        {
            _notifier.Notify("Deletion cancelled.", NotificationLevel.Info);
        }
    }
}
