using InventarySales.Models;
namespace InventarySales.Services
{
    public class ProductsServices
    {
        private static List<Product> products = new List<Product>{
            new Product { Id = 1, Name = "Product 1", Price = 10.99m, Stock = 100 },
            new Product { Id = 2, Name = "Product 2", Price = 20.99m, Stock = 50 },
            new Product { Id = 3, Name = "Product 3", Price = 15.99m, Stock = 75 }
        };

        

        public IEnumerable<Product> GetAllProducts() => products;
        public Product? GetProductById(int id) => products.FirstOrDefault(p => p.Id == id);

        public void AddProduct(Product product)
        {
            product.Id = products.Any() ? products.Max(p => p.Id) + 1 : 1;
            products.Add(product);
        }

        public void DeleteProduct(int id)
        {
            var product = GetProductById(id);
            if (product is not null)
                products.Remove(product);
        }

        public void UpdateProduct(int id, Product updatedProduct)
        {
            var existingProduct = GetProductById(id);
            if (existingProduct is not null)
            {
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.Stock = updatedProduct.Stock;
            }
        }

    }
}
