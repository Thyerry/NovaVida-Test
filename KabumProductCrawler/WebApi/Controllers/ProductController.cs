using Domain.Contracts.Service;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IWebCrawlerService _webCrawlerService;

        public ProductController(IProductService productService, IWebCrawlerService webCrawlerService)
        {
            _productService = productService;
            _webCrawlerService = webCrawlerService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _productService.Get());
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchProducts(string search)
        {
            var products = await _webCrawlerService.GetProductsFromKabum(search);
            await _productService.InsertOrUpdateProductsAsync(products);
            return Ok(products);
        }

        [HttpGet]
        [Route("reviews")]
        public async Task<IActionResult> GetProductReviews(int productId, int quantity = 5)
        {
            var productReviews = await _webCrawlerService.GetProductReviews(productId, quantity);
            return Ok(productReviews);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(ProductModel product)
        {
            await _productService.Insert(product);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductModel product)
        {
            await _productService.Update(product);
            return Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.Delete(id);
            return Accepted();
        }
    }
}