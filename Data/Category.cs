using System.ComponentModel.DataAnnotations;

namespace kargardoon.Data
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";
    }
}
