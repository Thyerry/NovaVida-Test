using Entity.Entity;

namespace Domain.Contracts.Repository;

public interface IProductRepository
{
    Task<List<Product>> Get();

    Task<Product?> GetById(int id);

    Task<Product> Insert(Product product);

    Task InsertChunk(List<Product> products);

    Task Update(Product product);
    
    Task UpdateChunk(List<Product> product);

    Task Delete(Product product);
}