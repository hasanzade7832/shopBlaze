using kargardoon.Data;
using kargardoon.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace kargardoon.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Category> CreateAsync(Category obj)
        {
            _db.Category.Add(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var obj = await _db.Category.FirstOrDefaultAsync(u => u.Id == id);
            if (obj == null) return false;

            _db.Category.Remove(obj);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<Category> GetAsync(int id)
        {
            return await _db.Category.FirstOrDefaultAsync(u => u.Id == id)
                ?? new Category(); // fallback
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _db.Category.ToListAsync();
        }

        public async Task<Category> UpdateAsync(Category obj)
        {
            var objFromDb = await _db.Category.FirstOrDefaultAsync(u => u.Id == obj.Id);
            if (objFromDb == null) return obj;

            objFromDb.Name = obj.Name;
            _db.Category.Update(objFromDb);
            await _db.SaveChangesAsync();

            return objFromDb;
        }
    }
}
