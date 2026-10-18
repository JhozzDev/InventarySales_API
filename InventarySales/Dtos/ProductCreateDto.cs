using Microsoft.AspNetCore.Mvc;

namespace InventarySales.Dtos
{
    public class ProductCreateDto 
    {
      public string Name { get; set; }
      public decimal Price { get; set; }
      public int CategoryId { get; set; }

      public int Stock { get; set; }
    }
}
