using AutoMapper;
using Domain.Contracts.Repository;
using Domain.Contracts.Service;
using Domain.Model;
using Entity.Entity;

namespace Domain.Service;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<List<ProductModel>> Get()
    {
        var products = await _productRepository.Get();
        return _mapper.Map<List<ProductModel>>(products);
    }

    public async Task<ProductModel> GetById(int id)
    {
        var product = await _productRepository.GetById(id);
        return _mapper.Map<ProductModel>(product);
    }

    public async Task<ProductModel> Insert(ProductModel productModel)
    {
        var product = _mapper.Map<Product>(productModel);
        var insertedProduct = await _productRepository.Insert(product);
        return _mapper.Map<ProductModel>(insertedProduct);
    }

    public async Task Update(ProductModel productModel)
    {
        var product = _mapper.Map<Product>(productModel);
        await _productRepository.Update(product);
    }

    public async Task Delete(int id)
    {
        var product = await _productRepository.GetById(id);
        if (product != null)
            await _productRepository.Delete(product);
    }

    public async Task InsertOrUpdateProductsAsync(List<ProductModel> productsModel)
    {
        var products = _mapper.Map<List<Product>>(productsModel);
        var productIds = products.Select(p => p.Id).ToList();

        var existingProducts = (await _productRepository.Get())
            .Where(p => productIds.Contains(p.Id))
            .ToList();

        var (productsToUpdate, productsToInsert) = SplitProductsToUpdateAndInsert(products, existingProducts);

        await UpdateProductsAsync(productsToUpdate, existingProducts);
        await InsertProductsAsync(productsToInsert);
    }

    private (List<Product> ProductsToUpdate, List<Product> ProductsToInsert) SplitProductsToUpdateAndInsert(List<Product> products, List<Product> existingProducts)
    {
        var productsToUpdate = products
            .Where(p => existingProducts.Any(ep => ep.Id == p.Id && !ep.Equals(p)))
            .ToList();

        var productsToInsert = products
            .Where(p => existingProducts.All(ep => ep.Id != p.Id))
            .ToList();

        return (productsToUpdate, productsToInsert);
    }

    private async Task UpdateProductsAsync(List<Product> productsToUpdate, List<Product> existingProducts)
    {
        if (!productsToUpdate.Any()) return;

        foreach (var productToUpdate in productsToUpdate)
        {
            var product = existingProducts.First(p => p.Id == productToUpdate.Id);
            product.Name = productToUpdate.Name;
            product.Price = productToUpdate.Price;
            product.Url = productToUpdate.Url;
            product.ImageUrl = productToUpdate.ImageUrl;

            await _productRepository.Update(product);
        }
    }

    private async Task InsertProductsAsync(List<Product> productsToInsert)
    {
        if (!productsToInsert.Any()) return;

        await _productRepository.InsertChunk(productsToInsert);
    }
}