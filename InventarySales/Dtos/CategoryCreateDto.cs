using System.ComponentModel.DataAnnotations;

namespace InventarySales.Dtos
{
    public class CategoryCreateDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
    }
}
