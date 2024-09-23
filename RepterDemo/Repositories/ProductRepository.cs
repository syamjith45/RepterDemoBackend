using Microsoft.EntityFrameworkCore;
using RepterDemo.Data;
using RepterDemo.Models;
using RepterDemo.DTO;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace RepterDemo.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> AddProductAsync(ProductDTO productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now // Initialize UpdatedAt here
            };

            if (productDto.Image != null)
            {
                product.ImageURL = await UploadImageAsync(productDto.Image);
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> UpdateProductAsync(int id, ProductDTO productDto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }

            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.UpdatedAt = DateTime.Now; // Only update UpdatedAt

            if (productDto.Image != null)
            {
                RemoveImage(product.ImageURL);
                product.ImageURL = await UploadImageAsync(productDto.Image);
            }

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            RemoveImage(product.ImageURL);

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }

        private async Task<string> UploadImageAsync(IFormFile image)
        {
            if (image == null) return null;

            var uploadsFolder = Path.Combine("wwwroot", "images");
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            Directory.CreateDirectory(uploadsFolder);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return $"/images/{uniqueFileName}";
        }

        private void RemoveImage(string imageURL)
        {
            if (!string.IsNullOrEmpty(imageURL))
            {
                var filePath = Path.Combine("wwwroot", imageURL.TrimStart('/'));
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }
    }
}
