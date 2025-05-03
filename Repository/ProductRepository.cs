using kargardoon.Data;
using kargardoon.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace kargardoon.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Product> CreateAsync(Product obj)
        {
            _db.Product.Add(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public async Task<Product> UpdateAsync(Product obj)
        {
            var objFromDb = await _db.Product.FirstOrDefaultAsync(u => u.Id == obj.Id);
            if (objFromDb == null) return obj;

            objFromDb.Name = obj.Name;
            objFromDb.Description = obj.Description;
            objFromDb.Price = obj.Price;
            objFromDb.SpecialTag = obj.SpecialTag;
            objFromDb.CategoryId = obj.CategoryId;
            objFromDb.ImageUrl = obj.ImageUrl;

            await _db.SaveChangesAsync();
            return objFromDb;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var obj = await _db.Product.FirstOrDefaultAsync(u => u.Id == id);
            if (obj == null) return false;

            _db.Product.Remove(obj);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<Product> GetAsync(int id)
        {
            return await _db.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(u => u.Id == id) ?? new Product();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _db.Product.Include(p => p.Category).ToListAsync();
        }
    }
}
