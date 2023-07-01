using Domain.Contracts.Repository;
using Entity.Entity;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly BaseContext _dbContext;

        public ProductRepository(BaseContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public async Task<List<Product>> Get()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetById(int id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> Insert(Product product)
        {
            var insertedProduct = await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return insertedProduct.Entity;
        }

        public async Task InsertChunk(List<Product> products)
        {
            await _dbContext.Products.AddRangeAsync(products);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task UpdateChunk(List<Product> productsToUpdate)
        {
            _dbContext.Products.UpdateRange(productsToUpdate);
            await _dbContext.SaveChangesAsync(); 
        }

        public async Task Delete(Product product)
        {
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}