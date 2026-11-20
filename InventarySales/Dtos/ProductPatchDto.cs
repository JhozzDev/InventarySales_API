
using System.ComponentModel.DataAnnotations;

namespace InventarySales.Dtos
{
    public class ProductPatchDto 
    {


      public decimal? Price { get; set; }


   
      [Range(1, int.MaxValue, ErrorMessage = "Stock must be a non-negative value")]
      public int? Stock { get; set; }
    }
}
