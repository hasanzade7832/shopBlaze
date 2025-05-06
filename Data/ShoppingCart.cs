using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kargardoon.Data
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }

        // 👤 User who added this item
        public string ApplicationUserId { get; set; } = "";

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; } = default!;

        // 📦 The selected product
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; } = default!;

        // 🔢 Quantity of the selected product
        [Range(1, 1000)]
        public int Count { get; set; }
    }
}
