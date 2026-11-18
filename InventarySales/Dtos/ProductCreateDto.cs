
using System.ComponentModel.DataAnnotations;

namespace InventarySales.Dtos
{
    public class ProductCreateDto 
    {

      [Required]
      [StringLength(100, MinimumLength = 2)]
      public string Name { get; set; }

      [Range(1, double.MaxValue, ErrorMessage = "Price must be a positive value")]
      public decimal Price { get; set; }
      [Required]
      [Range(1, int.MaxValue, ErrorMessage = "CategoryId must be a positive value")]
      public int CategoryId { get; set; }

      [Range(1, int.MaxValue, ErrorMessage = "Stock must be a non-negative value")]
      public int Stock { get; set; }
    }
}
