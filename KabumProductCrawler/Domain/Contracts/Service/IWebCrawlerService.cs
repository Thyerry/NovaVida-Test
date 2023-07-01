using Domain.Model;

namespace Domain.Contracts.Service;

public interface IWebCrawlerService
{
    Task<List<ProductModel>> GetProductsFromKabum(string productSearchTerm);

    Task<List<ProductReview>> GetProductReviews(int quantity);
}