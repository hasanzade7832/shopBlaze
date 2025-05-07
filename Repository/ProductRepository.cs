using kargardoon.Data;
using kargardoon.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace kargardoon.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public ProductRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            using var db = _dbFactory.CreateDbContext();

            return await db.Product
                .Include(p => p.Category)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product> GetAsync(int id)
        {
            using var db = _dbFactory.CreateDbContext();

            return await db.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(u => u.Id == id) ?? new Product();
        }

        public async Task<Product> CreateAsync(Product obj)
        {
            using var db = _dbFactory.CreateDbContext();
            db.Product.Add(obj);
            await db.SaveChangesAsync();
            return obj;
        }

        public async Task<Product> UpdateAsync(Product obj)
        {
            using var db = _dbFactory.CreateDbContext();
            db.Product.Update(obj);
            await db.SaveChangesAsync();
            return obj;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var db = _dbFactory.CreateDbContext();
            var obj = await db.Product.FindAsync(id);
            if (obj == null) return false;

            db.Product.Remove(obj);
            return await db.SaveChangesAsync() > 0;
        }
    }
}
