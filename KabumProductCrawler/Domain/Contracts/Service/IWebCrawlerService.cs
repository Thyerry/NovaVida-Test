using Domain.Model;

namespace Domain.Contracts.Service;

public interface IWebCrawlerService
{
    Task<List<ProductModel>> GetProductsFromKabum(string productSearchTerm);

    Task<ProductReviews> GetProductReviews(int productId, int quantity);
}