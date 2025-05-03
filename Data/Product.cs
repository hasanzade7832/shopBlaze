using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kargardoon.Data
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string Description { get; set; } = "";

        [Required]
        public double Price { get; set; }

        public string SpecialTag { get; set; } = "";

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = new();

        public string ImageUrl { get; set; } = "";
    }
}
