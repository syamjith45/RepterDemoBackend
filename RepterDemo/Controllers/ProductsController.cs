
using Microsoft.AspNetCore.Mvc;
using RepterDemo.DTO;
using RepterDemo.Models;
using RepterDemo.Repositories;


namespace RepterDemo.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetProductsAsync();
            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct([FromForm] ProductDTO productDto)
        {
            var product = await _productRepository.AddProductAsync(productDto);
            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductID }, product);
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductDTO productDto)
        {
            var updatedProduct = await _productRepository.UpdateProductAsync(id, productDto);
            if (updatedProduct == null) return NotFound();
            return NoContent();
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = await _productRepository.DeleteProductAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
