using Domain.Contracts.Repository;
using Entity.Entity;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class ProductRepository : IProductRepository, IDisposable
{
    private readonly DbContextOptions<BaseContext> _options;

    public ProductRepository()
    {
        _options = new DbContextOptions<BaseContext>();
    }

    public async Task<List<Product>> Get()
    {
        using (var database = new BaseContext(_options))
        {
            return await database.Products.ToListAsync();
        }
    }

    public async Task<Product?> GetById(int id)
    {
        using (var database = new BaseContext(_options))
        {
            return await database.Products.FirstOrDefaultAsync(p => p.Id == id);
        }
    }

    public async Task<Product> Insert(Product product)
    {
        using (var database = new BaseContext(_options))
        {
            var insertedProduct = await database.Products.AddAsync(product);
            await database.SaveChangesAsync();
            return insertedProduct.Entity;
        }
    }

    public async Task InsertChunk(List<Product> products)
    {
        using (var database = new BaseContext(_options))
        {
            await database.Products.AddRangeAsync(products);
            await database.SaveChangesAsync();
        }
    }

    public async Task Update(Product product)
    {
        using (var database = new BaseContext(_options))
        {
            database.Products.Update(product);
            await database.SaveChangesAsync();
        }
    }

    public async Task Delete(Product product)
    {
        using (var database = new BaseContext(_options))
        {
            database.Remove(product);
            await database.SaveChangesAsync();
        }
    }

    public void Dispose()
    {
    }
}