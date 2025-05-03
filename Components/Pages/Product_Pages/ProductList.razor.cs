using kargardoon.Data;
using kargardoon.Services;
using Microsoft.AspNetCore.Components;

namespace kargardoon.Components.Pages.Product_Pages
{
    public partial class ProductList
    {
        private bool isLoading;
        private bool showForm;
        private bool showConfirm;

        private List<Product> products = new();
        private List<Category> categories = new();
        private Product productModel = new();
        private int deletingId;

        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            products = await Manager.LoadProductsAsync();
            categories = (await CatRepo.GetAllAsync()).ToList();
            isLoading = false;
        }

        private async Task LoadData()
        {
            products = await Manager.LoadProductsAsync();
        }

        private void ShowAddForm()
        {
            productModel = new();
            showForm = true;
        }

        private void EditProduct(Product p)
        {
            productModel = new Product
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                SpecialTag = p.SpecialTag,
                CategoryId = p.CategoryId,
                ImageUrl = p.ImageUrl
            };
            showForm = true;
        }

        private async Task HandleSubmit()
        {
            await Manager.SaveAsync(productModel);
            showForm = false;
            await LoadData();
        }

        private void HideForm()
        {
            showForm = false;
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
            await LoadData();
        }

        private void CancelDelete()
        {
            showConfirm = false;
            Manager.NotifyCancelDelete();
        }
    }
}
