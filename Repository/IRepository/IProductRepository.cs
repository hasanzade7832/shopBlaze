using kargardoon.Data;

namespace kargardoon.Repository.IRepository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetAsync(int id);
        Task<Product> CreateAsync(Product obj);
        Task<Product> UpdateAsync(Product obj);
        Task<bool> DeleteAsync(int id);
    }
}
