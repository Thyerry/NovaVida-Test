using System.Collections.Generic;
using System.Threading.Tasks;
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

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _productService.Get());
        }

        [HttpPost]
        [Route("chunk")]
        public async Task<IActionResult> InsertChunk(List<ProductModel> products)
        {
            await _productService.InsertChunk(products);
            return Ok();
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