using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace kargardoon.Services.Extensions
{
    public static class IJSRuntimeExtensions
    {
        public static async Task ToasterSuccess(this IJSRuntime js, string message) =>
            await js.InvokeVoidAsync("showToaster", "success", message);

        public static async Task ToasterError(this IJSRuntime js, string message) =>
            await js.InvokeVoidAsync("showToaster", "error", message);

        public static async Task<bool> ConfirmDelete(this IJSRuntime js, string message) =>
            await js.InvokeAsync<bool>("showConfirm", message);
    }
}
