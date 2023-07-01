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

    public async Task InsertChunk(List<ProductModel> productsModel)
    {
        var products = _mapper.Map<List<Product>>(productsModel);
        await _productRepository.InsertChunk(products);
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
}