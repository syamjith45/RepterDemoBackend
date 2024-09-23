using RepterDemo.DTO;
using RepterDemo.Models;

namespace RepterDemo.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> AddProductAsync(ProductDTO productDto);
        Task<Product> UpdateProductAsync(int id, ProductDTO productDto);
        Task<bool> DeleteProductAsync(int id); // Change this to return Task<bool>
    }

}
