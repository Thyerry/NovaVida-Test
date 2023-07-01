using Domain.Model;

namespace Domain.Contracts.Service;

public interface IProductService
{
    Task<List<ProductModel>> Get();

    Task<ProductModel> GetById(int id);

    Task<ProductModel> Insert(ProductModel product);

    Task InsertOrUpdateProductsAsync(List<ProductModel> products);

    Task Update(ProductModel product);

    Task Delete(int id);
}