using kargardoon.Data;
using kargardoon.Services;
using Microsoft.AspNetCore.Components;

namespace kargardoon.Components.Pages.Category_Pages
{
    public partial class CategoryList
    {
        private bool isLoading;
        private bool showForm;
        private bool showConfirm;

        private List<Category> Categories = new();
        private Category categoryModel = new();
        private int deletingId;

        protected override async Task OnInitializedAsync() => await Load();

        private async Task Load()
        {
            isLoading = true;
            Categories = await Manager.LoadCategoriesAsync();
            isLoading = false;
        }

        private void ShowAddForm()
        {
            categoryModel = new();
            showForm = true;
        }

        private void EditCategory(Category c)
        {
            categoryModel = new Category { Id = c.Id, Name = c.Name };
            showForm = true;
        }

        private async Task HandleSubmit()
        {
            await Manager.SaveAsync(categoryModel);
            showForm = false;
            await Load();
        }

        private void PromptDelete(int id)
        {
            deletingId = id;
            showConfirm = true;
        }

        private async Task DeleteConfirmed()
        {
            await Manager.DeleteAsync(deletingId);
            showConfirm = false;
            await Load();
        }

        private void CancelDelete()
        {
            showConfirm = false;
            Manager.NotifyCancelDelete();
        }
    }
}
