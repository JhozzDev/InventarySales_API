
using System.ComponentModel.DataAnnotations;

namespace InventarySales.Dtos
{
    public class ProductCreateDto 
    {

        [Required(ErrorMessage = "Name is required")]
      [StringLength(100, MinimumLength = 2)]
      public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
      [Range(1, double.MaxValue, ErrorMessage = "Price must be a positive value")]
      public decimal Price { get; set; }

        [Required(ErrorMessage = "CategoryId is required")]
      [Range(1, int.MaxValue, ErrorMessage = "CategoryId must be a positive value")]
      public int CategoryId { get; set; }

        [Required(ErrorMessage = "Stock is required")]
      [Range(1, int.MaxValue, ErrorMessage = "Stock must be a non-negative value")]
      public int Stock { get; set; }
    }
}
