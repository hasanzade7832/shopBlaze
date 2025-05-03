using kargardoon.Data;
using kargardoon.Repository.IRepository;
using kargardoon.Services;

namespace kargardoon.Services
{
    public class CategoryCrudManager
    {
        private readonly ICategoryRepository _repo;
        private readonly NotificationService _notifier;

        public CategoryCrudManager(ICategoryRepository repo, NotificationService notifier)
        {
            _repo = repo;
            _notifier = notifier;
        }

        public async Task<List<Category>> LoadCategoriesAsync()
        {
            return (await _repo.GetAllAsync()).ToList();
        }

        public async Task<bool> SaveAsync(Category category)
        {
            if (category.Id == 0)
            {
                await _repo.CreateAsync(category);
                _notifier.Notify("Category added!", NotificationLevel.Success);
                return true;
            }
            else
            {
                await _repo.UpdateAsync(category);
                _notifier.Notify("Category updated!", NotificationLevel.Success);
                return false;
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
            _notifier.Notify("Category deleted!", NotificationLevel.Warning);
        }

        public void NotifyCancelDelete()
        {
            _notifier.Notify("Deletion cancelled.", NotificationLevel.Info);
        }
    }
}
